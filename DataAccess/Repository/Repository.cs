using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using System.Linq.Expressions;
using X.PagedList;

namespace DataAccess.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;
        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var inludeProperty in includes)
                {
                    query = query.Include(inludeProperty);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var inludeProperty in includes)
                {
                    query = query.Include(inludeProperty);
                }
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression, IList<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if(includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public void  Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

    }
}
