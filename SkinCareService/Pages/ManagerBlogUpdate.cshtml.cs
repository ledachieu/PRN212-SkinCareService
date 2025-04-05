using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class ManagerBlogUpdateModel : PageModel
    {
        private readonly BlogDAO _blogDAO;

        [BindProperty]
        public Blog Blog { get; set; }

        public ManagerBlogUpdateModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public IActionResult OnGet(int blogId)
        {
            Blog = _blogDAO.GetBlogById(blogId);
            return Blog == null ? RedirectToPage("/BlogListManager") : Page();
        }

        public IActionResult OnPost()
        {
            if (_blogDAO.UpdateBlog(Blog))
            {
                return RedirectToPage("/BlogListManager");
            }
            return Page();
        }
    }
}
