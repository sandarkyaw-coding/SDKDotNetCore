using SDKDotNetCoreNLayer.DataAccess.Database;
using SDKDotNetCoreNLayer.DataAccess.Models;

namespace SDKDotNetCoreNLayer.DataAccess.Services;

public class DataAccess_Blog
{
    private readonly AppDBContext _context;

    public DataAccess_Blog()
    {
        _context = new AppDBContext();
    }

    public List<BlogModel> getBlogs()
    {
        var lst = _context.Blogs.ToList();
        return lst;
    }

    public BlogModel getBlog(int id)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public int createBlog(BlogModel requestModel)
    {
        _context.Add(requestModel);
        var result = _context.SaveChanges();
        return result;
    }

    public int updateBlog(int id, BlogModel requestModel)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) { return 0; }

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        var result = _context.SaveChanges();
        return result;
    }

    public int patchBlog(int id, BlogModel requestModel)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        if (!string.IsNullOrEmpty(requestModel.BlogTitle))
        {
            item.BlogTitle = requestModel.BlogTitle;
        }
        if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            item.BlogAuthor = requestModel.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(requestModel.BlogContent))
        {
            item.BlogContent = requestModel.BlogContent;
        }
        var result = _context.SaveChanges();
        return result;
    }

    public int deleteBlog(int id)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return 0;
        }
        _context.Blogs.Remove(item);
        var result = _context.SaveChanges();
        return result;
    }
}
