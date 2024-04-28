using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.RestApi.Models;
using SDKDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.RestApi.Db
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
