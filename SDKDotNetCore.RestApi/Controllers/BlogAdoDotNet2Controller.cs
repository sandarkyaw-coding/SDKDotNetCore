using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SDKDotNetCore.RestApi.Models;
using SDKDotNetCore.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace SDKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.ConnectionStringBuilder.ConnectionString   );
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {

          string query = "select * from tbl_blog where BlogId = @BlogId";
            /*  //need to create array 
          AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
          //put these paramater on this array
          parameters[0] = new AdoDotNetParameter("@BlogId", id);

          var lst = _adoDotNetService.Query<BlogModel>(query, parameters); */

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            if(item is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog) {

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _adoDotNetService.Execute(query, 
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Creating Succcessful." : "Creating Failed.";
            return Ok(message);
        }

         [HttpPut("{id}")]
         public IActionResult UpdateBlog(int id, BlogModel blog)
         {
            string findIdQuery = "select * from tbl_blog where BlogId = @BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(findIdQuery, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No Data Found.");
            }
           
             string query = @"UPDATE [dbo].[Tbl_Blog]
    SET [BlogTitle] = @BlogTitle
       ,[BlogAuthor] = @BlogAuthor
       ,[BlogContent] = @BlogContent
  WHERE [BlogId] = @BlogId";
            var result = _adoDotNetService.Execute(query, 
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent));

             string message = result > 0 ? "Updating Successful." : "Updating Failed.";
             return Ok(message);
         }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            string findIdQuery = "select * from tbl_blog where BlogId = @BlogId";

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(findIdQuery, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            List<AdoDotNetParameter> parameters = new List<AdoDotNetParameter>();
            parameters.Add(new AdoDotNetParameter("@BlogId", id));

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, " ;
                parameters.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
 
            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
                parameters.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
                parameters.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            if(conditions.Length == 0)
            {
                return NotFound("No Data to Update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [BlogId] = @BlogId";

            var result = _adoDotNetService.Execute(query, parameters.ToArray());

            string message = result > 0 ? "Patch Updating Successful." : "Patch Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string findIdQuery = "select * from tbl_blog where BlogId = @BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(findIdQuery, new AdoDotNetParameter("@BlogId", id));

            if (item == null)
            {
                return NotFound("No Data Found.");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [BlogId] = @BlogId";
            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
      
    }
}
