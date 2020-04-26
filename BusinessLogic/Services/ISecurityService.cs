
namespace BusinessLogic.Services
{
    public interface ISecurityService
    {
        string HashPassword(string password);
        bool CheckPassword(string password, string hashed);

    }
}
