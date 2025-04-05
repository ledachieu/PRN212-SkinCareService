using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class ManagerBlogCreateModel : PageModel
    {
        private readonly BlogDAO _blogDAO;

        [BindProperty]
        public Blog Blog { get; set; } = new Blog();

        public ManagerBlogCreateModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public IActionResult OnPost()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToPage("/Login");
            }
            Blog.CreatedAt = DateTime.Now;
            Blog.AuthorId = UserId; 

            if (_blogDAO.CreateBlog(Blog))
            {
                return RedirectToPage("/BlogListManager");
            }
            return Page();
        }
    }
}
