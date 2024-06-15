using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SDKDotNetCore.ConsoleApp.AdoDotNetExamples;
using SDKDotNetCore.ConsoleApp.DapperExamples;
using SDKDotNetCore.ConsoleApp.EFCoreExamples;
using SDKDotNetCore.ConsoleApp.Services;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("C#", "Dardar", "C# Learning");
//adoDotNetExample.Update(1, "C# Update", "Sandar Kyaw", "Update C# Learning");
//adoDotNetExample.Delete(1002);
//adoDotNetExample.Edit(1);
//adoDotNetExample.Edit(1);

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

var connectionString = ConnectionStrings.ConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDBContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDBContext db = serviceProvider.GetRequiredService<AppDBContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Delete(2028);

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadKey();