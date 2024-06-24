using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.MvcApp.Models;

namespace SDKDotNetCore.MvcApp.Db
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
} */
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
