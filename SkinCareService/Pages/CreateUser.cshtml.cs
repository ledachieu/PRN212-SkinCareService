using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public CreateUserModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [BindProperty]
        public User User { get; set; }

        public bool CreateError { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!_userDAO.CreateUser(User))
            {
                CreateError = true;
                return Page();
            }

            return RedirectToPage("/UserList");
        }
    }
}
