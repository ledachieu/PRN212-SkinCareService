using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class AddResultModel : PageModel
    {
        private readonly ServiceResultDAO _serviceResultDAO;
        private readonly TherapistDAO _therapistDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddResultModel(ServiceResultDAO serviceResultDAO,TherapistDAO therapistDAO, IHttpContextAccessor httpContextAccessor)
        {
            _serviceResultDAO = serviceResultDAO;
            _therapistDAO = therapistDAO;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public int BookingId { get; set; }

        [BindProperty]
        public string ResultDescription { get; set; }

        public void OnGet(int bookingId)
        {
            var roleId = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
            if (roleId != 3)
            {
                RedirectToPage("/Login");
            }
            BookingId = bookingId;
        }

        public IActionResult OnPost()
        {
            var roleId = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
            var userId = HttpContext.Session.GetInt32("UserId");
            if (roleId != 3 || userId == null)
            {
                RedirectToPage("/Login");
            }
            if (string.IsNullOrWhiteSpace(ResultDescription))
            {
                ModelState.AddModelError("", "Mô tả không được để trống.");
                return Page();
            }


            var therapist = _therapistDAO.GetTherapistByUserId((int)userId);
            if (therapist == null)
            {
                return NotFound();
            }

            var result = new ServiceResult
            {
                BookingId = BookingId,
                TherapistId = therapist.TherapistId,
                ResultDescription = ResultDescription,
                ResultDate = DateTime.Now
            };

            _serviceResultDAO.AddServiceResult(result);
            return RedirectToPage("ResultList", new { bookingId = BookingId });
        }
    }
}
