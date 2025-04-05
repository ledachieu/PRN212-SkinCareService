using Microsoft.EntityFrameworkCore;
using Models;
using SkinCareService.Models;

namespace DAO
{
    public class ServiceDAO
    {
        private readonly SkinServiceContext _context;

        public ServiceDAO(SkinServiceContext context)
        {
            _context = context;
        }
        public bool CreateService(SkinService service)
        {
            try
            {
                service.Status = 1;
                _context.SkinServices.Add(service);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Get all services
        public List<SkinService> GetAllServices()
        {
            return _context.SkinServices.ToList();
        }

        // Get a service by ID
        public SkinService GetServiceByIdAdmin(int serviceId)
        {
            return _context.SkinServices.Find(serviceId);
        }

        // Update a service
        public bool UpdateService(int serviceId, SkinService service)
        {
            try
            {
                var existingService = _context.SkinServices.Find(serviceId);
                if (existingService != null)
                {
                    existingService.ServiceName = service.ServiceName;
                    existingService.Description = service.Description;
                    existingService.Price = service.Price;
                    existingService.Duration = service.Duration;
                    existingService.Status = service.Status;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Ban a service (set Status to 0)
        public bool BanService(int serviceId)
        {
            try
            {
                var service = _context.SkinServices.Find(serviceId);
                if (service != null)
                {
                    service.Status = 0; // Banned status
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Unban a service (set Status to 1)
        public bool UnbanService(int serviceId)
        {
            try
            {
                var service = _context.SkinServices.Find(serviceId);
                if (service != null)
                {
                    service.Status = 1; // Active status
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(List<SkinService>, int)> GetServicesAsync(int page, int pageSize)
        {
            var query = _context.SkinServices.Where(X => X.Status == 1).AsQueryable();
            int totalCount = await query.CountAsync();
            var services = await query
                .OrderBy(s => s.ServiceId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (services, totalCount);
        }
        public SkinService? GetServiceById(int id)
        {
            return _context.SkinServices.Include(x => x.Bookings).ThenInclude(x => x.Feedbacks).FirstOrDefault(s => s.ServiceId == id);
        }

        public List<Feedback> GetFeedbacks(SkinService? service)
        {
            return service.Bookings
                 .SelectMany(b => b.Feedbacks)
                 .OrderByDescending(f => f.CreatedAt)
                 .ToList();
        }
        public List<SkinService> GetLatestServices(int count) =>
        _context.SkinServices
                .OrderByDescending(s => s.CreatedAt)
                .Take(count)
                .ToList();
    }
}
