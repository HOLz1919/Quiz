using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly ApplicationDbContext _db;

        public ChallengeService(ApplicationDbContext db)
        {
            this._db = db;
        }



        public async Task<ResponseDto> AddAsync(Challenge challenge)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                await _db.Challenges.AddAsync(challenge);
                await _db.SaveChangesAsync();
                responseDto.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                var challenge = _db.Challenges.FirstOrDefault(item => item.Id == id);
                if (challenge != null)
                {
                    _db.Challenges.Remove(challenge);
                    await _db.SaveChangesAsync();
                    responseDto.IsSuccessful = true;
                }
                else
                {
                    responseDto.IsSuccessful = false;
                    responseDto.ErrorMessage = "Challenge with that Id doesn`t exists";
                }
                
            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> EditAsync(Challenge challenge)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                Challenge toEdit = _db.Challenges.FirstOrDefault(item => item.Id == challenge.Id);
                if (toEdit != null)
                {
                    toEdit.CategoryId = challenge.CategoryId;
                    toEdit.Content = challenge.Content;
                    toEdit.Count = challenge.Count;
                    toEdit.Reward = challenge.Reward;
                    toEdit.Title = challenge.Title;
                    await _db.SaveChangesAsync();
                    responseDto.IsSuccessful = true;
                }
                else
                {
                    responseDto.IsSuccessful = false;
                    responseDto.ErrorMessage = "Challenge with that Id doesn`t exists";
                }

            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<UserMoneyDto> EndChallenge(UserChallenge userChallenge)
        {
            UserMoneyDto userMoneyDto = new UserMoneyDto();
            try
            {
                var userChallengeMap = await _db.UserChallenges.FirstOrDefaultAsync(item => item.ChallengeId == userChallenge.ChallengeId && item.ApplicationUserId == userChallenge.ApplicationUserId);
                if (userChallengeMap != null)
                {
                    userChallengeMap.Status = 2;
                }
                else
                {
                    userMoneyDto.ErrorMessage = "Not found user Challenge";
                    userMoneyDto.IsSuccessful = false;
                    return userMoneyDto;
                }

                var reward =  _db.Challenges.FirstOrDefault(item => item.Id == userChallenge.ChallengeId).Reward;
                var user = await _db.Users.FirstOrDefaultAsync(item => item.Id == userChallenge.ApplicationUserId);
                user.Money += reward;
                await _db.SaveChangesAsync();
                userMoneyDto.IsSuccessful = true;
                userMoneyDto.Money = reward;

            }
            catch(Exception ex)
            {
                userMoneyDto.IsSuccessful = false;
                userMoneyDto.Money = 0;
                userMoneyDto.ErrorMessage = ex.Message;
            }

            return userMoneyDto;
        }

        public async Task<Challenge> GetAsync(Guid id)
        {
            var challenge = await _db.Challenges.SingleAsync(item => item.Id == id);
            if (challenge != null)
            {
                challenge.UserChallenges = null;
                challenge.Category = null;
            }
            return challenge != null ? challenge : null;
        }

        public async Task<List<ChallengeView>> GetAsync()
        {
            var result = await _db.ChallengeViews.ToListAsync();
            return result;
        }

        public async Task<List<ChallengeUserView>> GetChallengeUserAsync(string userId)
        {
            try
            {

                var challengesIds =  _db.Challenges.Select(item => item.Id);
                var userChallenges = _db.UserChallenges.Where(item => item.ApplicationUserId == userId).Select(item => item.ChallengeId);
                var notContainsChallenges = challengesIds.Where(item => !userChallenges.Contains(item));
                List<UserChallenge> userChallengesToAdd = new List<UserChallenge>();
                foreach(var item in notContainsChallenges)
                {
                    userChallengesToAdd.Add(
                        new UserChallenge()
                    {
                        ChallengeId = item,
                        ApplicationUserId = userId,
                        Status = 1
                    }
                    );
                }

                //var challengesNotMappedWithUser = await (from c in _db.Challenges
                //                                         join u in _db.UserChallenges on c.Id equals u.ChallengeId into uj
                //                                         from x in uj.DefaultIfEmpty()
                //                                         where  x.ApplicationUserId != userId
                //                                         select new UserChallenge{ 
                //                                            ChallengeId=c.Id,
                //                                            ApplicationUserId=userId,
                //                                            Status=1 
                //                                         }).ToListAsync();

                if (userChallengesToAdd.Count > 0)
                {
                    await _db.UserChallenges.AddRangeAsync(userChallengesToAdd);
                    await _db.SaveChangesAsync();
                }

                var challengesUserView = await (from c in _db.Challenges
                                                join u in _db.UserChallenges on c.Id equals u.ChallengeId
                                                join r in _db.ResultsViews on new
                                                {
                                                    Key1 = c.CategoryId,
                                                    Key2 = u.ApplicationUserId
                                                }
                                                equals new
                                                {
                                                    Key1 = r.CategoryId,
                                                    Key2 = r.ApplicationUserId
                                                }
                                                into result
                                                from x in result.DefaultIfEmpty()
                                                where u.ApplicationUserId == userId
                                                select new ChallengeUserView
                                                {
                                                    ChallengeId = c.Id,
                                                    Title = c.Title,
                                                    Content = c.Content,
                                                    Count = c.Count,
                                                    Reward = c.Reward,
                                                    UserId = userId,
                                                    WonMatches = x.WonMatches,
                                                    Status = u.Status

                                                }).ToListAsync();

                foreach(var item in challengesUserView)
                {
                    if (!item.WonMatches.HasValue)
                        item.WonMatches = 0;
                    if (item.WonMatches.Value >= item.Count)
                    {
                        item.Percentage = 100.0;
                    }
                    else
                    {
                        item.Percentage = item.WonMatches.Value * 100.0 / item.Count;
                    }
                
                }

                            return challengesUserView;


            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ChallengeUserView>();
            }
        }
    }
}
