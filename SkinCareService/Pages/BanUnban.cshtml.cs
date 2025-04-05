using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class BanUnbanModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public BanUnbanModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public IActionResult OnGet(int userId)
        {
            _userDAO.ToggleBanStatus(userId);
            return RedirectToPage("/UserList");
        }
    }
}
