using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Helpers;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IUserService :IService<User>
    {

        User GetOne(int id);
        User GetOne(string email);
        Task<User> GetOneAsync(int id);
        Task<User> GetOneAsync(string email);
        PagedList<User> GetAll(UserResourceParameters resourceParameters);
    }
}
