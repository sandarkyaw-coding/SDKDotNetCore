using Microsoft.EntityFrameworkCore;

namespace SDKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BusinessLogic_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BusinessLogic_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blBlog.getBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _blBlog.createBlog(blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            var result = _blBlog.updateBlog(id, blog);
 
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            var result = _blBlog.patchBlog(id, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            
            int result = _blBlog.deleteBlog(id);

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
