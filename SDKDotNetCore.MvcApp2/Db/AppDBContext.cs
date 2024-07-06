using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.MvcApp2.Models;

namespace SDKDotNetCore.MvcApp2.Db
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
