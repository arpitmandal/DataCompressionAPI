using Microsoft.EntityFrameworkCore;

namespace DataCompressionAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Define DbSet properties for each table in the database
        public DbSet<User> Users { get; set; }
        public DbSet<FileLog> FileLogs { get; set; }
    }
}