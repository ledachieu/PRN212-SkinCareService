using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class BlogDetailModel : PageModel
    {
        private readonly BlogDAO _blogDAO;

        public BlogDetailModel(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Blog = await _blogDAO.GetBlogByIdAsync(id);

            if (Blog == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
