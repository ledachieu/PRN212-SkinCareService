using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class BlogListManagerModel : PageModel
    {
        private readonly BlogDAO _blogDAO;
        public List<Blog> Blogs { get; set; } = new List<Blog>();
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public BlogListManagerModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public void OnGet(int pageNumber = 1)
        {
            const int pageSize = 5; // Số blog trên mỗi trang
            int totalBlogs;

            Blogs = _blogDAO.GetBlogs(pageNumber, pageSize, out totalBlogs);
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalBlogs / (double)pageSize);
        }
    }
}
