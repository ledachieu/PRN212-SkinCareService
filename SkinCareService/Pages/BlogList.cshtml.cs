using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class BlogListModel : PageModel
    {
        private readonly BlogDAO _blogDAO;

        public BlogListModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public async Task OnGetAsync()
        {
            Blogs = await _blogDAO.GetAllBlogsAsync();
        }
    }
}
