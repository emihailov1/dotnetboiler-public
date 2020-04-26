using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IService<T> where T : class
    {

        IQueryable<T> GetAll();
        EntityEntry<T> Add(T entity);
        Task<EntityEntry<T>> AddAsync(T entity);
        void Update(T entity);
        EntityEntry<T> Delete(T entity);
        bool Save();
    }
}
