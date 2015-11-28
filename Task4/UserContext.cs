using System.Data.Entity;

namespace Task4
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
