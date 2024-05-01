using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDKDotNetCore.ConsoleApp.Services;
using SDKDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SDKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(item);

        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
           string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Creating Successful." : "Creating Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";

            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if(conditions.Length == 0)
            {
                return NotFound("No Data to Update");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            string query = @"Delete [dbo].[Tbl_Blog] WHERE [BlogId] = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
