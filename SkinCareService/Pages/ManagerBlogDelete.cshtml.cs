using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class ManagerBlogDeleteModel : PageModel
    {
        private readonly BlogDAO _blogDAO;

        public ManagerBlogDeleteModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public IActionResult OnGet(int blogId)
        {
            if (_blogDAO.DeleteBlog(blogId))
            {
                return RedirectToPage("/BlogListManager");
            }
            return Page();
        }
    }
}
