using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class TherapistDAO
    {
        private readonly SkinServiceContext _context;
        public TherapistDAO(SkinServiceContext context) => _context = context;
        public List<Therapist> GetLatestTherapists(int count) =>
        _context.Therapists
                .Include(t => t.User)
                .OrderByDescending(t => t.TherapistId)
                .Take(count)
                .ToList();
        public List<Therapist> GetAll()
        {
            return _context.Therapists
                           .Include(t => t.User) // Include User if needed for FullName, etc.
                           .ToList();
        }
        public Therapist? GetTherapistByUserId(int userId)
        {
            return _context.Therapists.FirstOrDefault(t => t.UserId == userId);
        }
    }

}
