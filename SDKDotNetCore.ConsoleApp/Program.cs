using SDKDotNetCore.ConsoleApp.EFCoreExamples;

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

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

Console.ReadKey();