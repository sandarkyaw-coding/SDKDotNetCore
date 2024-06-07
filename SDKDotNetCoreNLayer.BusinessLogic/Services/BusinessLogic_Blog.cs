using SDKDotNetCoreNLayer.DataAccess.Models;
using SDKDotNetCoreNLayer.DataAccess.Services;

namespace SDKDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BusinessLogic_Blog
    {
        private readonly DataAccess_Blog _daBlog;

        public BusinessLogic_Blog()
        {
            _daBlog = new DataAccess_Blog();
        }

        public List<BlogModel> getBlogs()
        {
            var lst = _daBlog.getBlogs();
            return lst;
        }

        public BlogModel getBlog(int id)
        {
            var item = _daBlog.getBlog(id);
            return item;
        }

        public int createBlog(BlogModel requestModel)
        {
            var result = _daBlog.createBlog(requestModel);
            return result;
        }

        public int updateBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.updateBlog(id, requestModel);
            return result;
        }

        public int patchBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.patchBlog(id, requestModel);
            return result;
        }

        public int deleteBlog(int id)
        {
            var result = _daBlog.deleteBlog(id);
            return result;
        }
    }
}
