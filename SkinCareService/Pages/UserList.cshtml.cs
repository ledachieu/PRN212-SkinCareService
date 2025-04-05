using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class UserListModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public UserListModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public List<User> Users { get; set; }

        public void OnGet()
        {
            Users = _userDAO.ListCustomers();
        }
    }
}
