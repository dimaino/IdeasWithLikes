using Microsoft.EntityFrameworkCore;
 
namespace BlackBeltTest2.Models
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }
        public DbSet<User> User {get; set;}
    }
}