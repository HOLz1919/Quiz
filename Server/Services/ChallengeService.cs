using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
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

        public async Task<List<Challenge>> GetAsync()
        {
            var result = await _db.Challenges.ToListAsync();
            result.ForEach(item => { item.UserChallenges = null; item.Category = null; });
            return result;
        }
    }
}
