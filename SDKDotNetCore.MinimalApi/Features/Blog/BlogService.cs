using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.MinimalApi.Db;
using SDKDotNetCore.MinimalApi.Models;

namespace SDKDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlogs(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/Blog", async (AppDBContext db) =>
            {
                var lst = await db.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("api/Create", async (AppDBContext db, BlogModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving successful." : "Saving Failed.";
                return Results.Ok(message);
            });

            app.MapPut("api/UpdatePut/{id}", async (AppDBContext db, int id, BlogModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No Data Found.");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                return Results.Ok(message);
            });

            app.MapDelete("api/Delete/{id}", async (AppDBContext db, int id) =>
            {
                var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No Data Found.");
                }

                db.Blogs.Remove(item);

                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

                return Results.Ok(message);
            });

            return app;
        }
    }
}
