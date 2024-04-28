using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKDotNetCore.ConsoleApp.Dtos;

namespace SDKDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        private readonly AppDBContext db = new AppDBContext();

        public void Run()
        {
            Read();
            //Edit(1);
            //Edit(12);
            Create("EFCore 3", "dar dar 3", "EFCore Example Again...");
            //Update(7, "EFCore 2 Update", "dar dar update", "Update Content");
            //Delete(6);
        }

        private void Read()
        {

            var lst = db.Blogs.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-------------------");
            }
        }

        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-------------------");

        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }

    }
}
