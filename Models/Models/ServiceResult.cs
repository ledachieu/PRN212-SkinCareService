using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class ServiceResult
{
    public int ResultId { get; set; }

    public int? BookingId { get; set; }

    public int? TherapistId { get; set; }

    public string? ResultDescription { get; set; }

    public DateTime? ResultDate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Therapist? Therapist { get; set; }
}
