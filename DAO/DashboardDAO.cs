using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class DashboardDAO
    {
        private readonly SkinServiceContext _context;

        public DashboardDAO(SkinServiceContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCustomersAsync()
        {
            return await _context.Users.CountAsync(u => u.RoleId == 2);
        }

        public async Task<int> GetCompletedBookingsAsync()
        {
            return await _context.Bookings.CountAsync(b => b.Status == "Completed");
        }

        public async Task<int> GetTotalSkinServicesAsync()
        {
            return await _context.SkinServices.CountAsync();
        }

        public async Task<int> GetTotalFeedbacksAsync()
        {
            return await _context.Feedbacks.CountAsync();
        }

        public async Task<List<Booking>> GetRecentBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .OrderByDescending(b => b.BookingDate)
                .Take(5) // Lấy 5 booking gần nhất
                .ToListAsync();
        }
    }
}
