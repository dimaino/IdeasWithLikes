using Microsoft.EntityFrameworkCore;
 
namespace LoginAndRegisterFinal.Models
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }
        public DbSet<User> User {get; set;}
        public DbSet<Message> Message {get; set;}
    }
}