using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class StaffAction
{
    public int ActionId { get; set; }

    public int? BookingId { get; set; }

    public int? StaffId { get; set; }

    public string? ActionType { get; set; }

    public DateTime? ActionDate { get; set; }

    public string? Notes { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual User? Staff { get; set; }
}
