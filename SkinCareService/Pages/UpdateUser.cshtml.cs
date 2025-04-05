using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class UpdateUserModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public UpdateUserModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [BindProperty]
        public User User { get; set; }

        public bool UpdateError { get; set; }

        public void OnGet(int userId)
        {
            // Fetch user details based on userId passed in the query parameter
            User = _userDAO.GetUserById(userId);
            if (User == null)
            {
                RedirectToPage("/Error");
            }
        }

        public IActionResult OnPost(int userId)
        {
            var existingUser = _userDAO.GetUserById(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Prevent email update
            User.Email = existingUser.Email;

            bool isUpdated = _userDAO.UpdateUser(userId, User);

            if (isUpdated)
            {
                return RedirectToPage("/UserList"); // Redirect back to the user list page
            }
            else
            {
                UpdateError = true;
                return Page(); // Stay on the update page if there's an error
            }
        }
    }
}
