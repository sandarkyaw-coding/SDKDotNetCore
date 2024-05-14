using SDKDotNetCore.HttpClientExamples;

Console.WriteLine("Hello, World!");

//Console App - Client (frontend)
//ASP .Net Core Web API - Server (backend)

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();