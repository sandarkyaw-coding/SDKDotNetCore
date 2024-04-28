using Dapper;
using SDKDotNetCore.ConsoleApp.Dtos;
using SDKDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            // Read();
            // Edit(1);
            //  Edit(2);
            //Create("dapper title", "dapper author", "dapper test content");
            // Update(2 ,"dapper update", "dapper update", "dapper test update content");
            Delete(4);
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from tbl_blog").ToList();
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
            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            var item = db.Query("select * from tbl_blog where blogid = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();

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
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };
            string query = @"Delete [dbo].[Tbl_Blog] WHERE [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }

}
