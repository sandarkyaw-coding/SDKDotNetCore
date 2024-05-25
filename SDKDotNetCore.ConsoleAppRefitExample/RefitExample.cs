using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.ConsoleAppRefitExample
{
    public class RefitExample
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7197");
        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(2);
            //await EditAsync(34);
            //await CreateAsync("dar dar", "Hello", "testing ");
           //await UpdateAsync(1, "dar dar", "Hello", "Sorry For Late");
           //await DeleteAsync(2);
        }

        public async Task ReadAsync()
        {
            var lst = await _service.GetBlogs();

            foreach (var blog in lst)
            {
                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Author => {blog.BlogAuthor}");
                Console.WriteLine($"Content => {blog.BlogContent}");
            }
        }

        public async Task EditAsync(int id)
        {
            try
            {      
                var item = await _service.GetBlog(id);
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch(Exception ex) 
            {
             Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

           var message = await _service.CreateBlog(blog);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                };

                var message = await _service.UpdateBlog(id, blog);
                Console.WriteLine(message);

            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var message = await _service.DeleteBlog(id);
                Console.WriteLine(message);

            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }
    }
} 