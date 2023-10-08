
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class  GenericRepository<T> where T : BaseEntity
{    
        private readonly APIAuthTwoStepContext _context;

        public GenericRepository(APIAuthTwoStepContext context)
        {
            _context = context;
        }

        protected virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression is not null){
                return await _context.Set<T>().Where(expression).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        } 
        public async virtual Task<T> FindFirst(Expression<Func<T, bool>> expression)
        {
            if (expression != null){
                var rst = await _context.Set<T>().Where(expression).ToListAsync();
                return rst.First();
            }
            return await _context.Set<T>().FirstAsync();
        }

        


        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public virtual IEnumerable<T> Find(Expression<Func<T,bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        
        
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }



    // GetAllAsync

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await GetAll();
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression) => await GetAll(expression);
        public virtual async Task<IEnumerable<T>> GetAllAsync(IParam param) => await GetAllPaginated(param);
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, IParam param) => await GetAllPaginated(param, expression);


    // Paginacion

        protected virtual bool PaginateExpression(T entity, string Search)=> true;
        private async Task<IEnumerable<T>> GetAllPaginated(IParam param, Expression<Func<T, bool>> expression = null)
        {
            return (await GetAll(expression))
                    .Where(x => PaginateExpression(x,param.Search))
                    .Skip((param.PageIndex - 1) * param.PageSize)
                    .Take(param.PageSize)
                    .ToList();
        }
    
}
