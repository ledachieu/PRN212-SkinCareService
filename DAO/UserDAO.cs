using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class UserDAO
    {
        private readonly SkinServiceContext _context;

        public UserDAO(SkinServiceContext context)
        {
            _context = context;
        }

        // Lấy người dùng theo Email
        public User GetUserByEmailPassword(string email, string password)
        {
            return _context.Users
                .Include(u => u.Role)  // Nếu cần thông tin role
                .FirstOrDefault(u => u.Email == email && u.PasswordHash.Equals(password) && u.Status == 1);
        }
        public User GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Role)  // Nếu cần thông tin role
                .FirstOrDefault(u => u.UserId == id);
        }
        // Create User (Ensure Email is Unique)
        public bool CreateUser(User user)
        {
            // Check if the email already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return false; // Email already exists, return false
            }

            // Set the default status (active, for example)
            user.Status = 1; // 0 for active
            user.RoleId = 2;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        // Update User (Prevent Changing Email)
        public bool UpdateUser(int userId, User updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (existingUser == null)
            {
                return false; // User not found
            }

            // Ensure the email is not updated
            updatedUser.Email = existingUser.Email;

            // Update other fields
            existingUser.FullName = updatedUser.FullName;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Address = updatedUser.Address;
            existingUser.RoleId = updatedUser.RoleId;
            existingUser.Status = updatedUser.Status;
            existingUser.CreatedAt = updatedUser.CreatedAt;
            existingUser.RoleId = 2;

            _context.SaveChanges();
            return true;
        }

        // Toggle Ban/Unban User
        public bool ToggleBanStatus(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false; // User not found
            }

            // Toggle the status between banned (1) and active (0)
            user.Status = user.Status == 1 ? 0 : 1;

            _context.SaveChanges();
            return true;
        }

        // List Users with RoleId = 2 (Customers)
        public List<User> ListCustomers()
        {
            return _context.Users.Where(u => u.RoleId == 2).ToList();
        }
    }
}
