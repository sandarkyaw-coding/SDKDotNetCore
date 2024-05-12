using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SDKDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace SDKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            //  List<BlogModel> lst = new List<BlogModel>();
            //  foreach (DataRow dr in dt.Rows)
            // {
            // BlogModel blog = new BlogModel();
            // blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            // blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            // blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            // blog.BlogContent = Convert.ToString(dr["BlogContent"]);

            // BlogModel blog = new BlogModel {
            //   BlogId = Convert.ToInt32(dr["BlogId"]),
            //     BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //     BlogContent = Convert.ToString(dr["BlogContent"])
            //  };
            // blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            // blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            // blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            // blog.BlogContent = Convert.ToString(dr["BlogContent"]);

            // lst.Add(blog);


            // }

            // no looping method
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();


            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "select * from tbl_blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found.");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog) {
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Creating Succcessful." : "Creating Failed.";
            return Ok(message);
        }

         [HttpPut("{id}")]
         public IActionResult UpdateBlog(int id, BlogModel blog)
         {
             SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
             connection.Open();
             string findIdQuery = "select count(*) from Tbl_Blog where BlogId = @BlogId";
             SqlCommand findIdCmd = new SqlCommand(findIdQuery, connection);
             findIdCmd.Parameters.AddWithValue("@BlogId", id);
            var item = (int)findIdCmd.ExecuteScalar();
             if (item == 0)
             {
                 return NotFound("No Data Found");
             }

             string query = @"UPDATE [dbo].[Tbl_Blog]
    SET [BlogTitle] = @BlogTitle
       ,[BlogAuthor] = @BlogAuthor
       ,[BlogContent] = @BlogContent
  WHERE [BlogId] = @BlogId";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@BlogId", id);
             cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
             cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
             cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
             int result = cmd.ExecuteNonQuery();

             connection.Close();

             string message = result > 0 ? "Updating Successful." : "Updating Failed.";
             return Ok(message);
         }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {

            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            connection.Open();
            string findIdQuery = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand findIdCmd = new SqlCommand(findIdQuery, connection);
            findIdCmd.Parameters.AddWithValue("@BlogId", id);
            int item = (int)findIdCmd.ExecuteScalar();
            if (item == 0)
            {
                return NotFound("No Data Found");
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(findIdCmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            
            DataRow dr = dt.Rows[0];

            List<BlogModel> lst = new List<BlogModel>();
            var bmodel  = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
            lst.Add(bmodel);

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, " ;

            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
            }

            if(conditions.Length == 0)
            {
                return NotFound("No Data to Update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [BlogId] = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);

            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Patch Updating Successful." : "Patch Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
            connection.Open();
            string findByIdQuery = "select count(*) from tbl_blog where BlogId = @BlogId";
            SqlCommand findByIdCmd = new SqlCommand(findByIdQuery, connection);
            findByIdCmd.Parameters.AddWithValue("@BlogId", id);
            var item = (int)findByIdCmd.ExecuteScalar();
            if(item == 0)
            {
                return NotFound("No Data Found");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [BlogId] = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            var result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
      
    }
}
