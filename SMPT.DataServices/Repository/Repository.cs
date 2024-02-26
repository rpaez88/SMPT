using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using SMPT.Entities.DbSet;
using SMPT.DataServices.Data;

namespace SMPT.DataServices.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ILogger _logger;
        protected AppDbContext _context;
        internal DbSet<T> _dbSet;

        public Repository(ILogger logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            if (entity is BaseEntity e)
            {
                e.Id = Guid.NewGuid();
                e.CreatedDate = DateTime.Now;
                e.UpdatedDate = DateTime.Now;
            }
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await GetById(id);
                if (result == null)
                    return false;

                _dbSet.Remove(result);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function", typeof(Repository<T>));
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _dbSet
                    .AsNoTracking()
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function", typeof(Repository<T>));
                throw;
            }
            
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T?> Find(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<bool> Update(T entity)
        {
            try
            {
                if (entity is BaseEntity e)
                {
                    e.UpdatedDate = DateTime.Now;
                }
                _dbSet.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function: Repository", "Repository");
                throw;
            }
        }
    }
}
