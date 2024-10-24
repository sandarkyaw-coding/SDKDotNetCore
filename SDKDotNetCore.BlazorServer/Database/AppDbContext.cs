using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.BlazorServer.Models;

namespace SDKDotNetCore.BlazorServer.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
