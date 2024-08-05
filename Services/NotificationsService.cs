using Jobeer.Models;
using Jobber.Data;
using Jobber.Services.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jobeer.Services
{
    public interface INotificationsService : IEntityGetter<NotifCache>, IEntitySetter<NotifCache>
    {
    };

    public class NotificationsService : INotificationsService
    {
        private readonly DataContext _dbContext;
        public NotificationsService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NotifCache> Get(Expression<Func<NotifCache, bool>> predicate)
        {
            var data = await _dbContext.NotifCache.FirstOrDefaultAsync(predicate);
            return data;
        }

        public async Task<NotifCache> GetById(int Id)
        {
            var data = await _dbContext.NotifCache.FirstOrDefaultAsync(s => s.Id == Id);
            return data;
        }

        public async Task<IEnumerable<NotifCache>> GetRange(Expression<Func<NotifCache, bool>> predicate)
        {
            var data = _dbContext.NotifCache.Where(predicate);
            return data;
        }

        public async Task<IEnumerable<NotifCache>> GetAll()
        {
            var data = _dbContext.NotifCache;
            return data;
        }

        public async Task Remove(Expression<Func<NotifCache, bool>> predicate)
        {
            var data = await _dbContext.NotifCache.FirstOrDefaultAsync(predicate);
            _dbContext.Remove(data);
            _dbContext.SaveChanges();
        }

        public async Task RemoveRange(Expression<Func<NotifCache, bool>> predicate)
        {
            var data = _dbContext.NotifCache.Where(predicate);
            _dbContext.RemoveRange(data);
            _dbContext.SaveChanges();
        }

        public async Task Add(NotifCache newEntity)
        {
            _dbContext.Add(newEntity);
            _dbContext.SaveChanges();
        }

        public async Task Update(NotifCache notif)
        {
            var data = _dbContext.NotifCache.FirstOrDefault(g => g.Key == notif.Key);

            if (data != null)
            {
                _dbContext.NotifCache.Attach(data);
                data = notif;
                _dbContext.SaveChanges();
            }
        }

        public async Task UpdateOrAdd(NotifCache notif)
        {
            var data = _dbContext.NotifCache.FirstOrDefault(g => g.Key == notif.Key);

            if (data != null)
            {
                _dbContext.NotifCache.Attach(data);

                data = notif;
            }
            else
            {
                await _dbContext.AddAsync(notif);
            }

            _dbContext.SaveChanges();
        }
    }
}
