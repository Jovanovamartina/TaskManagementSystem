
using Application_TaskManagement.IServices;

namespace Application_TaskManagement.Services
{
    public class BCryptPasswordHasher  : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hashedPassword, string plainPassword) =>
            BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}
