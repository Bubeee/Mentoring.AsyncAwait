using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Task4
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<User>> GetAll()
        {
            using (var db = new UserContext())
            {
                var users = await db.Users.ToListAsync();
                return users;
            }
        }

        public async Task<int> CreateAsync(User user)
        {
            using (var db = new UserContext())
            {
                var addedUser = db.Users.Add(user);
                db.SaveChanges();
                return addedUser.UserId;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var db = new UserContext())
            {
                var user = await db.Users.FindAsync(id);
                return user;
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using (var db = new UserContext())
            {
                var result = await db.Users.SingleOrDefaultAsync(e => e.UserId == user.UserId);
                if (result != null)
                {
                    result.Name = user.Name;
                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var db = new UserContext())
            {
                var user = await db.Users.FindAsync(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}
