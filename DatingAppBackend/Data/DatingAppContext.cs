using DatingAppBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppBackend.Data
{
    public class DatingAppContext(DbContextOptions<DatingAppContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
