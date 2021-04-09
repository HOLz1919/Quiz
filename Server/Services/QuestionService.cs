using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public class QuestionService : IQuestionService
    {

        private readonly ApplicationDbContext _db;

        public QuestionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseDto> AddAsync(QuestionVM questionVM)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                Question question = new Question();
                question.Id = Guid.NewGuid();
                question.Content = questionVM.Content;
                question.CategoryId = questionVM.CategoryId;
                question.Answers = questionVM.Answers;

                await _db.Questions.AddAsync(question);
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
                var question = _db.Questions.FirstOrDefault(item => item.Id == id);
                if (question != null)
                {
                    _db.Questions.Remove(question);
                    await _db.SaveChangesAsync();
                    responseDto.IsSuccessful = true;
                }
                else
                {
                    responseDto.IsSuccessful = false;
                    responseDto.ErrorMessage = "Category with that Id doesn`t exists";
                }

            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> EditAsync(QuestionVM questionVM)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                Question toEdit = _db.Questions.FirstOrDefault(item => item.Id == questionVM.Id);
                toEdit.Answers = _db.Answers.Where(item => item.QuestionId == toEdit.Id).ToList();
                if (toEdit != null)
                {
                    toEdit.Content = questionVM.Content;
                    toEdit.CategoryId = questionVM.CategoryId;
                    _db.Answers.RemoveRange(toEdit.Answers);
                    toEdit.Answers = questionVM.Answers;
                    await _db.SaveChangesAsync();
                    responseDto.IsSuccessful = true;
                }
                else
                {
                    responseDto.IsSuccessful = false;
                    responseDto.ErrorMessage = "Category with that Id doesn`t exists";
                }

            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<QuestionView>> GetAsync()
        {
            try
            {
                var result = await _db.QuestionViews.ToListAsync();
                return result;
            }catch(Exception ex)
            {
                return null;
            }
           
        }

        public async Task<QuestionVM> GetAsync(Guid id)
        {
            var Question = await _db.Questions.SingleAsync(item => item.Id == id);
            List<Answer> answers = await _db.Answers.Where(item => item.QuestionId == Question.Id).ToListAsync();
            answers.ForEach(item => item.Question = null);
            QuestionVM QuestionVM = new QuestionVM()
            {
                Id = Question.Id,
                CategoryId = Question.CategoryId,
                Content = Question.Content,
                Answers = answers
            };
            return QuestionVM != null ? QuestionVM : null;
        }


    }
}
