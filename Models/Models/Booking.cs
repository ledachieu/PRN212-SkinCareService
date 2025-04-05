using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public int? ServiceId { get; set; }

    public int? TherapistId { get; set; }

    public DateTime BookingDate { get; set; }

    public string? Status { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual SkinService? Service { get; set; }

    public virtual ICollection<ServiceResult> ServiceResults { get; set; } = new List<ServiceResult>();

    public virtual ICollection<StaffAction> StaffActions { get; set; } = new List<StaffAction>();

    public virtual Therapist? Therapist { get; set; }
}
