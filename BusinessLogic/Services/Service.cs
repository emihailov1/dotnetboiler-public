using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public abstract class Service<C, T> : IService<T> where T : class where C : DbContext, new()
    {

        protected readonly ApiDbContext context;
        protected readonly IPropertyMappingService propertyMappingService;

        public Service(ApiDbContext context, IPropertyMappingService propertyMappingService)
        {
            this.context = context;
            this.propertyMappingService = propertyMappingService;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = context.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);
            return query;
        }

        public virtual EntityEntry<T> Add(T entity)
        {
           EntityEntry<T> result = context.Set<T>().Add(entity);
           return result;
        }

        public virtual Task<EntityEntry<T>> AddAsync(T entity)
        {
           Task<EntityEntry<T>> result =  context.Set<T>().AddAsync(entity);
           return result;
        }

        public virtual void Update (T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public virtual EntityEntry<T> Delete(T entity)
        {
            EntityEntry<T> result = context.Set<T>().Remove(entity);
            return result;
        }

        public virtual bool Save()
        {
            return context.SaveChanges() < 1 ? false : true;
        }

        public virtual Task<T> GetOneAsync(int id)
        {
            Task<T> result =  context.Set<T>().FirstOrDefaultAsync(x => (int)(typeof(T).GetProperty("ID").GetValue(id)) == id);
            return result;
        }
    }
}
