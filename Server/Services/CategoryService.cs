using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<ResponseDto> AddAsync(Category category)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync();
                responseDto.IsSuccessful = true;
            }
            catch(Exception ex)
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
                var category = _db.Categories.FirstOrDefault(item => item.Id == id);
                if (category != null)
                {
                    _db.Categories.Remove(category);
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

        public async Task<ResponseDto> EditAsync(Category category)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                Category toEdit = _db.Categories.FirstOrDefault(item => item.Id == category.Id);
                if (toEdit != null)
                {
                    toEdit.Name = category.Name;
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

        public async  Task<List<Category>> GetAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(Guid id)
        {
            var category = await _db.Categories.SingleAsync(item => item.Id == id);
            return category != null ? category : null;
        }
    }
}
