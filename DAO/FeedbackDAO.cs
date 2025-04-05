using Models;
using SkinCareService.Models;

namespace DAO
{
    public class FeedbackDAO
    {
        private readonly SkinServiceContext _context;

        public FeedbackDAO(SkinServiceContext context)
        {
            _context = context;
        }

        public void Add(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
    }
}
