using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class BookSkinServiceModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceDAO _skinServiceDAO;
        private readonly TherapistDAO _therapistDAO;
        private readonly BookingDAO _bookingDAO;
        private readonly WorkingScheduleDAO _workingScheduleDAO;

        public SkinService? Service { get; set; }
        public List<Therapist> Therapists { get; set; } = new();
        [BindProperty]
        public DateOnly? WorkDate { get; set; }

        public BookSkinServiceModel(IHttpContextAccessor httpContextAccessor,ServiceDAO serviceDAO,TherapistDAO therapistDAO,BookingDAO bookingDAO,WorkingScheduleDAO workingScheduleDAO)
        {
            _httpContextAccessor = httpContextAccessor;
            _skinServiceDAO = serviceDAO;
            _therapistDAO = therapistDAO;
            _bookingDAO = bookingDAO;
            _workingScheduleDAO = workingScheduleDAO;
        }

        public IActionResult OnGet(int serviceId)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");
            Service = _skinServiceDAO.GetServiceById(serviceId);
            Therapists = _therapistDAO.GetAll();
            if (Service == null) return RedirectToPage("/SkinCareList");
            return Page();
        }

        public IActionResult OnPost(int serviceId)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            Service = _skinServiceDAO.GetServiceById(serviceId);
            Therapists = _therapistDAO.GetAll();

            var form = Request.Form;
            var date = DateOnly.Parse(form["WorkDate"]);
            var start = TimeOnly.Parse(form["StartTime"]);
            var end = TimeOnly.Parse(form["EndTime"]);
            int therapistId = int.Parse(form["TherapistId"]);

            // Kiểm tra lịch làm việc bị trùng
            bool isConflict = _workingScheduleDAO.IsConflict(therapistId, date, start, end);
            if (isConflict)
            {
                ModelState.AddModelError("", "Chuyên viên đã có lịch trong khoảng thời gian này.");
                return Page();
            }

            // Tạo Booking
            var booking = new Booking
            {
                CustomerId = userId,
                ServiceId = serviceId,
                TherapistId = therapistId,
                BookingDate = DateTime.Now,
                Status = "Pending",
                PaymentStatus = "Not Paid",
                CreatedAt = DateTime.Now
            };
            _bookingDAO.Add(booking);

            // Tạo WorkingSchedule
            var schedule = new WorkingSchedule
            {
                TherapistId = therapistId,
                WorkDate = date,
                StartTime = start,
                EndTime = end
            };
            _workingScheduleDAO.Add(schedule);

            return RedirectToPage("/MyBooking");
        }
    }
}
