using System.Threading.Tasks;

namespace Task4
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);

        Task<User> GetByIdAsync(int id);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);
    }
}
