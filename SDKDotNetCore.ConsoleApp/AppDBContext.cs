using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.ConsoleApp
{
    internal class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
