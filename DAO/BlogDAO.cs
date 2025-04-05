using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class BlogDAO
    {
        private readonly SkinServiceContext _context;

        public BlogDAO(SkinServiceContext context)
        {
            _context = context;
        }
        // Create Blog
        public List<Blog> GetBlogs(int pageNumber, int pageSize, out int totalBlogs)
        {
            totalBlogs = _context.Blogs.Count();
            return _context.Blogs
                .Include(b => b.Author)
                .OrderByDescending(b => b.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Blog> GetLatestBlogs(int count) =>
       _context.Blogs.Include(b => b.Author)
                     .OrderByDescending(b => b.CreatedAt)
                     .Take(count)
                     .ToList();

        public bool CreateBlog(Blog blog)
        {
            try
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool UpdateBlog(Blog blog)
        {
            try
            {
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteBlog(int blogId)
        {
            try
            {
                var blog = _context.Blogs.Find(blogId);
                if (blog == null) return false;

                _context.Blogs.Remove(blog);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // Get Blog by Id
        public Blog GetBlogById(int blogId)
        {
            return _context.Blogs.Find(blogId);
        }

        // Lấy tất cả các blog
        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs
                .Include(b => b.Author)  // Kéo thông tin tác giả của blog
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // Lấy blog chi tiết theo BlogId
        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Author)  // Kéo thông tin tác giả của blog
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }
    }
}
