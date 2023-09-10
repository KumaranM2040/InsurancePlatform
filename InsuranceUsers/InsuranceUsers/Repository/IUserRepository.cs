using InsuranceUsers.Repository.Models;

namespace InsuranceUsers.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> GetAllUsers();

        Task<IEnumerable<UserDetails>> GetUserByUsername(string Username);

        Task<int> Register(UserDetails userDetails);
    }
}
