using Demo.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories.Base
{
    public class GenericRepository<TEntity> where TEntity : Entity
    {
        #region Fields

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;

        #endregion

        public GenericRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        #region Public Methods

        public Task<TEntity> GetById(int id) => FirstOrDefault(x=>x.Id == id);

        public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _set.FirstOrDefaultAsync(predicate);

        public async Task Add(TEntity entity)
        {
            // await Context.AddAsync(entity);
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(TEntity entity)
        {
            // In case AsNoTracking is used
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task Remove(TEntity entity)
        {
            _set.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => _set.CountAsync();

        public Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
            => _set.CountAsync(predicate);

        #endregion

    }
}
