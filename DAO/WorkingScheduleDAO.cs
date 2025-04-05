using Models;
using SkinCareService.Models;

namespace DAO
{
    public class WorkingScheduleDAO
    {
        private readonly SkinServiceContext _context;

        public WorkingScheduleDAO()
        {
            _context = new SkinServiceContext();
        }

        public void Add(WorkingSchedule schedule)
        {
            _context.WorkingSchedules.Add(schedule);
            _context.SaveChanges();
        }

        public bool IsConflict(int therapistId, DateOnly date, TimeOnly start, TimeOnly end)
        {
            return _context.WorkingSchedules.Any(w =>
                w.TherapistId == therapistId &&
                w.WorkDate == date &&
                (
                    start >= w.StartTime && start < w.EndTime ||
                    end > w.StartTime && end <= w.EndTime ||
                    start <= w.StartTime && end >= w.EndTime
                ));
        }
    }
}
