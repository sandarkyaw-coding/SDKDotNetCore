using Microsoft.EntityFrameworkCore;
using SDKDotNetCoreNLayer.DataAccess.Models;

namespace SDKDotNetCoreNLayer.DataAccess.Database
{
    internal class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
