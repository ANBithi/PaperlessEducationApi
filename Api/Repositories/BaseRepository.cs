using Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : AbstractDbEntity
    {
        protected IDbContext _context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IDbContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await DbSet.FindAsync(predicate);
            return data.ToList();
        }
        public virtual async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await DbSet.FindAsync(predicate);
            return data.SingleOrDefault();
        }
        public virtual void Add(TEntity obj)
        {
            _context.AddCommand(() => DbSet.InsertOneAsync(obj));            
        }
        public virtual void AddMany(IEnumerable<TEntity> objs)
        {
            _context.AddCommand(() => DbSet.InsertManyAsync(objs));
        }
        public virtual async Task<TEntity> GetById(string id)
        {
            var data = await DbSet.FindAsync(x => x.Id == id);
            return data.SingleOrDefault();
        }

        public virtual Task<IQueryable<TEntity>> GetAll()
        {
            var all = DbSet.AsQueryable<TEntity>();
            return Task.FromResult<IQueryable<TEntity>>(all);
        }
        public async virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await DbSet.FindAsync(predicate);
            return data.ToList();
        }
        public virtual List<dynamic> GetSingleColumnList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> selectPredicate)
        {
            var data = DbSet.Find(predicate)
                            .Project<TEntity>(Builders<TEntity>.Projection.Include(selectPredicate)).ToEnumerable();
            var result = data.Select(selectPredicate.Compile()).ToList();
            return result;
        }
        public virtual IEnumerable<TEntity> GetAllByIds(List<string> ids)
        {
            var data = DbSet.Find(x => ids.Contains(x.Id));
            return data.ToEnumerable();
        }

        public virtual void Update(TEntity obj)
        {
            AbstractDbEntity dbEntity = obj as AbstractDbEntity;
            _context.AddCommand(() => DbSet.ReplaceOneAsync(x => x.Id == dbEntity.Id, obj));
        }
        public virtual void UpdateMany(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, dynamic>> updatePredicate, dynamic value)
        {
            var update = Builders<TEntity>.Update.Set<dynamic>(updatePredicate, value);
            var filter = Builders<TEntity>.Filter.Where(filterPredicate);
            _context.AddCommand(() => DbSet.UpdateManyAsync(filter, update));
        }
        public virtual void ReplaceManyAsync(IEnumerable<TEntity> entities)
        {
            var updates = new List<WriteModel<TEntity>>();
            var filterBuilder = Builders<TEntity>.Filter;

            foreach (var doc in entities)
            {
                var filter = filterBuilder.Where(x => x.Id == doc.Id);
                updates.Add(new ReplaceOneModel<TEntity>(filter, doc));
            }
            _context.AddCommand(() => DbSet.BulkWriteAsync(updates));
        }
        public async virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var filter = Builders<TEntity>.Filter.Where(predicate);
            var count = await DbSet.CountDocumentsAsync(filter);
            return count;
        }
        public virtual void Remove(string id)
        {
            _context.AddCommand(() => DbSet.DeleteOneAsync(x => x.Id == id));
        }
        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            _context.AddCommand(() => DbSet.DeleteOneAsync(predicate));
        }
        public virtual void RemoveMany(Expression<Func<TEntity, bool>> predicate)
        {
            _context.AddCommand(async () => await DbSet.DeleteManyAsync(predicate));
        }
        public Task<int> Commit()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Reset()
        {
            _context?.Reset();
        }

        public void SetDbContext(IDbContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
