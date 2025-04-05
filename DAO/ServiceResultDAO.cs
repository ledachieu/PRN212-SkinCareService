using Models;
using SkinCareService.Models;

namespace DAO
{
    public class ServiceResultDAO
    {
        private readonly SkinServiceContext _context;

        public ServiceResultDAO()
        {
            _context = new SkinServiceContext();
        }

        public List<ServiceResult> GetResultsByBookingId(int bookingId)
        {
            return _context.ServiceResults
                .Where(r => r.BookingId == bookingId)
                .Select(r => new ServiceResult
                {
                    ResultId = r.ResultId,
                    BookingId = r.BookingId,
                    TherapistId = r.TherapistId,
                    ResultDescription = r.ResultDescription,
                    ResultDate = r.ResultDate,
                    Therapist = new Therapist
                    {
                        TherapistId = r.Therapist.TherapistId,
                        User = new User
                        {
                            FullName = r.Therapist.User.FullName
                        }
                    }
                })
                .ToList();
        }


        public void AddServiceResult(ServiceResult result)
        {
            _context.ServiceResults.Add(result);
            _context.SaveChanges();
        }
    }
}
