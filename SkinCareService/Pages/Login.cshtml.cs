using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DAO;

namespace SkinCareService.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserDAO _userDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(UserDAO userDAO, IHttpContextAccessor httpContextAccessor)
        {
            _userDAO = userDAO;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public bool LoginError { get; set; }
        public IActionResult OnGet()
        {
            
            return Page();
        }

        public IActionResult OnPost()
        {
            var acc = _userDAO.GetUserByEmailPassword(Email, Password);
            if (acc != null)
            {
                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", acc.UserId);
                _httpContextAccessor.HttpContext.Session.SetString("UserEmail", acc.Email);
                _httpContextAccessor.HttpContext.Session.SetInt32("RoleId", acc.RoleId ?? 0);
                return RedirectToPage("/Index"); // Redirect đến trang chủ hoặc trang khác sau khi login thành công
            }

            LoginError = true;
            return Page();
        }
    }
}
