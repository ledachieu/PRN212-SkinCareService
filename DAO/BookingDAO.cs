using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class BookingDAO
    {
        private readonly SkinServiceContext _context;


        public BookingDAO()
        {
            _context = new SkinServiceContext();
        }
        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
        public List<BookingViewModel> GetBookingsByCustomerId(int customerId)
        {
            var result = (from b in _context.Bookings
                          join ss in _context.SkinServices on b.ServiceId equals ss.ServiceId
                          join t in _context.Therapists on b.TherapistId equals t.TherapistId
                          join u in _context.Users on t.UserId equals u.UserId
                          join ws in _context.WorkingSchedules on b.TherapistId equals ws.TherapistId
                          where b.CustomerId == customerId
                          select new BookingViewModel
                          {
                              BookingId = b.BookingId,
                              ServiceName = ss.ServiceName,
                              TherapistName = u.FullName,
                              BookingDate = b.BookingDate,
                              WorkDate = ws.WorkDate,
                              StartTime = ws.StartTime,
                              EndTime = ws.EndTime,
                              PaymentStatus = b.PaymentStatus,
                              Status = b.Status
                          }).ToList();

            return result;
        }
        public Booking? GetById(int bookingId)
        {
            return _context.Bookings
                .Where(b => b.BookingId == bookingId)
                .Select(b => new Booking
                {
                    BookingId = b.BookingId,
                    CustomerId = b.CustomerId,
                    ServiceId = b.ServiceId,
                    TherapistId = b.TherapistId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                    PaymentStatus = b.PaymentStatus,
                    CreatedAt = b.CreatedAt
                })
                .FirstOrDefault();
        }
        public List<Booking> GetBookingsByTherapistId(int therapistId)
        {
            return _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .Where(b => b.TherapistId == therapistId)
                .ToList();
        }
        public List<Booking> GetAllBookingsForStaff()
        {
            return _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }
        public void MarkAsPaid(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null && booking.PaymentStatus == "Not Paid")
            {
                booking.PaymentStatus = "Paid";
                _context.SaveChanges();
            }
        }

        public void CancelBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null && booking.Status != "Completed")
            {
                booking.Status = "Cancelled";
                _context.SaveChanges();
            }
        }
        public void MarkAsCompleted(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null && booking.PaymentStatus == "Paid" && booking.Status != "Cancelled" && booking.Status != "Completed")
            {
                booking.Status = "Completed";
                _context.SaveChanges();
            }
        }
    }
}
