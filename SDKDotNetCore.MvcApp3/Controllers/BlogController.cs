using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.MvcApp3.Database;

namespace SDKDotNetCore.MvcApp3.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        //contructor injection
        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        //[ActionName("Index")]
        //public IActionResult BlogIndex([FromServices] AppDbContext db) //method injection
        // {
        //   return View("BlogIndex");
        // }

        // [FromServices]
        // public AppDbContext db { get; set; } //property injection 
        [HttpGet]
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            List<BlogEntity> lst =  _db.Blogs.AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToList();
            return View("BlogIndex", lst);
        }

        [HttpGet]
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        //save
        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogEntity blog)
        {
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Json(new {Message = message, IsSuccess = result > 0});
        }

        //Edit
        [HttpGet]
        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if(item is null) return RedirectToAction("Index");
            
            return View("BlogEdit", item);
        }

        //update
        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogEntity blog)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return Json(new { Message = "No Data Found.", IsSuccess = false});

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Json(new { Message = message, IsSuccess = result > 0 });
        }

        //delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogEntity blog)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blog.BlogId);
            if (item is null) return Json(new { Message = "No Data Found.", IsSuccess = false });

            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Json(new { Message = message, IsSuccess = result > 0 });
        }

    }
}
