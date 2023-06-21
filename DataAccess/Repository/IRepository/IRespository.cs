using Models.Model;
using System.Linq.Expressions;
using X.PagedList;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(
            Expression<Func<T, bool>>? expresssion = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null);
        Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string>? includes = null);
        Task<T> Get(Expression<Func<T, bool>> expression, IList<string>? includes = null);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
