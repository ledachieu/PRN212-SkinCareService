using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly DashboardDAO _dashboardDAO;

        public int TotalCustomers { get; private set; }
        public int CompletedBookings { get; private set; }
        public int TotalSkinServices { get; private set; }
        public int TotalFeedbacks { get; private set; }
        public List<Booking> RecentBookings { get; private set; } = new();

        public DashboardModel(DashboardDAO dashboardDAO)
        {
            _dashboardDAO = dashboardDAO;
        }

        public async Task OnGetAsync()
        {
            TotalCustomers = await _dashboardDAO.GetTotalCustomersAsync();
            CompletedBookings = await _dashboardDAO.GetCompletedBookingsAsync();
            TotalSkinServices = await _dashboardDAO.GetTotalSkinServicesAsync();
            TotalFeedbacks = await _dashboardDAO.GetTotalFeedbacksAsync();
            RecentBookings = await _dashboardDAO.GetRecentBookingsAsync();
        }
    }
}
