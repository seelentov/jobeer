using Jobeer.Models;
using Jobber.Data;
using Jobber.Services.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jobeer.Services
{
    public interface ISearchModelsService : IEntityGetter<SearchModel>
    {
        public Task CheckLastParse(Expression<Func<SearchModel, bool>> predicate);
    };

    public class SearchModelsService: ISearchModelsService
    {
        private readonly DataContext _dbContext;
        public SearchModelsService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SearchModel> Get(Expression<Func<SearchModel, bool>> predicate)
        {
            var data = await _dbContext.SearchModels.FirstOrDefaultAsync(predicate);
            return data;
        }

        public async Task<SearchModel> GetById(int Id)
        {
            var data = await _dbContext.SearchModels.FirstOrDefaultAsync(s=>s.Id == Id);
            return data;
        }

        public async Task<IEnumerable<SearchModel>> GetRange(Expression<Func<SearchModel, bool>> predicate)
        {
            var data = _dbContext.SearchModels.Where(predicate);
            return data;
        }

        public async Task<IEnumerable<SearchModel>> GetAll()
        {
            var data = _dbContext.SearchModels;
            return data;
        }

        public async Task CheckLastParse(Expression<Func<SearchModel, bool>> predicate)
        {
            var data = await _dbContext.SearchModels.FirstOrDefaultAsync(predicate);

            if(data != null)
            {
                data.LastParse = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
        }
    }
}
