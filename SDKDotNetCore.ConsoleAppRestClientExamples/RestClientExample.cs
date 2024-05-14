using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SDKDotNetCore.ConsoleAppRestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7197/"));
        private readonly string _blogEndPoint = "api/blog";
        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(3);
            //await EditAsync(2024);

            //await CreateAsynsc("dar dar", "rad rad", "dara dara");
            //await UpdatePutAsync(3,"doo doo", "dee dee", "dae dae");
            //await DeleteAsync(23);
        }

        private async Task ReadAsync()
        {
            //RestRequest restRequest = new RestRequest(_blogEndPoint);
            // var response = await _client.GetAsync(restRequest);

            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);

                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"Title => {blog.BlogTitle}");
                    Console.WriteLine($"Author => {blog.BlogAuthor}");
                    Console.WriteLine($"Content => {blog.BlogContent}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsynsc(string title, string author, string content)
        {
            BlogModel blogModel = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            }; //C# Object

            var restRequest = new RestRequest(_blogEndPoint, Method.Post);
            restRequest.AddJsonBody(blogModel);

            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content!;
                Console.WriteLine(result);
            }
            else
            {
                var message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task UpdatePutAsync(int id, string title, string author, string content)
        {
            BlogModel blogModel = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            var restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blogModel);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content!;
                Console.WriteLine(result);
            }
            else
            {
                var message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}");
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content!;
                Console.WriteLine(result);
            }
            else
            {
                var message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
