using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.ConsoleApp.Dtos;
using SDKDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.ConsoleApp.EFCoreExamples
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
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
