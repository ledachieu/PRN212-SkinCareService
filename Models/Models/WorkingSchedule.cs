using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class WorkingSchedule
{
    public int ScheduleId { get; set; }

    public int? TherapistId { get; set; }

    public DateOnly WorkDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Therapist? Therapist { get; set; }
}
