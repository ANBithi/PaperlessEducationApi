using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(string id);
        Task<IQueryable<TEntity>> GetAll();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllByIds(List<string> keys);
        void Update(TEntity obj);
        void Remove(string id);
        void RemoveMany(Expression<Func<TEntity, bool>> predicate);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        Task<int> Commit();
        void Reset();
        void UpdateMany(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, dynamic>> updatePredicate, dynamic value);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        void ReplaceManyAsync(IEnumerable<TEntity> entities);
        void AddMany(IEnumerable<TEntity> objs);
        List<dynamic> GetSingleColumnList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> selectPredicate);

    }
}
