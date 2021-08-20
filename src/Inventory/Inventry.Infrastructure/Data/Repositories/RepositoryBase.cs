using AutoMapper;
using Inventory.Infrastructure.Data.TDO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data.Repositories
{
    public class RepositoryBase<T, TEntity> : IAsyncRepository<T> where TEntity : EntityBase 
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;

        public RepositoryBase(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
           // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var entities = await _dbContext.Set<TEntity>().ToListAsync();
            var orderEntity = _mapper.Map<IReadOnlyList<T>>(entities);
            return orderEntity;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var predicateEnntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var outputEntity = await _dbContext.Set<TEntity>().Where(predicateEnntity).ToListAsync();
            var output = _mapper.Map<IReadOnlyList<T>>(outputEntity);
            return output;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            var predicateEnntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            if (predicate != null) query = query.Where(predicateEnntity);

            var queryEnntity = _mapper.Map<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(orderBy);
            IReadOnlyList<T> output;
            if (orderBy != null) {
                output = _mapper.Map<IReadOnlyList<T>>(await queryEnntity(query).ToListAsync());
            }
            else
            {
                output = _mapper.Map <IReadOnlyList<T>>(await query.ToListAsync());
            }

            return output; 
        }

        //public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        //{
        //    IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        //    if (disableTracking) query = query.AsNoTracking();

        //    if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null)
        //        return await orderBy(query).ToListAsync();
        //    return await query.ToListAsync();
        //}

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            var obj = _mapper.Map<T>(entity);
            return obj;
        }

        public async Task<T> AddAsync(T obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            _dbContext.Set<TEntity>().Add(entity);

            var newEntity = await _dbContext.SaveChangesAsync();

            obj = _mapper.Map<T>(newEntity);
            return obj;
        }

        public async Task UpdateAsync(T obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
