using BLueCodeChanllenge.Context;
using BLueCodeChanllenge.Interfaces;
using BLueCodeChanllenge.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace BLueCodeChanllenge.Services
{
    public class DbService<TEntity> : IDbService<TEntity> where TEntity : class
    {
        private BlueContext _context;

       
        public DbService(BlueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
           
        }

        public int GetNextId()
        {
            var lastEntity = _context.Set<TEntity>().OrderByDescending(e => e).LastOrDefault();

            if (lastEntity != null)
            {
                var idProperty = lastEntity.GetType().GetProperty("Id");
                var lastId = (int)idProperty.GetValue(lastEntity);
                return lastId + 1;
            }

            return 1;
        }
        public void Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

      
        public IEnumerable<TEntity> GetAll()
        {
           return _context.Set<TEntity>();
        }
       
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
