using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> conditionExpression);
    }
}
