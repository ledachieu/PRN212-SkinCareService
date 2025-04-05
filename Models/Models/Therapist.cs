using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class Therapist
{
    public int TherapistId { get; set; }

    public int? UserId { get; set; }

    public string? Specialization { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Certifications { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ServiceResult> ServiceResults { get; set; } = new List<ServiceResult>();

    public virtual User? User { get; set; }

    public virtual ICollection<WorkingSchedule> WorkingSchedules { get; set; } = new List<WorkingSchedule>();
}
