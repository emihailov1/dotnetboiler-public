
namespace BusinessLogic.Services
{
    public class SecurityService : ISecurityService
    {


        public string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(6);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        public bool CheckPassword(string password, string hashed)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(password, hashed);
            return isValid;
        }

    }
}
