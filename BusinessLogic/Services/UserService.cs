using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public sealed class UserService : Service<ApiDbContext,User>, IUserService
    {
        public UserService(ApiDbContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        public User GetOne(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.ID == id);
            return user;
        }

        public User GetOne(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<User> GetOneAsync(string email)
        {
            var user =  await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetOneAsync(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.ID == id);
            return user;
        }

        public PagedList<User> GetAll(UserResourceParameters resourceParameters)
        {

            var collectionBeforePaging = context.Users.ApplySort(resourceParameters.OrderBy, propertyMappingService.GetPropertyMapping<User>());

            if (!string.IsNullOrEmpty(resourceParameters.Name))
            {
                var titleForWhereClause = resourceParameters.Name
                  .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(x => x.FirstName.ToLowerInvariant() == titleForWhereClause);
            }

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQueryWhereClause = resourceParameters.SearchQuery
                  .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(a => a.FirstName.ToLowerInvariant().Contains(searchQueryWhereClause)
                || a.LastName.ToLowerInvariant().Contains(searchQueryWhereClause) || a.Email.ToLowerInvariant().Contains(searchQueryWhereClause));
            }

            return PagedList<User>.Create(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);
        }
    }
}
