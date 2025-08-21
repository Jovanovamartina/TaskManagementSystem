
namespace Application_TaskManagement.IServices
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hashedPassword, string plainPassword);
    }
}
