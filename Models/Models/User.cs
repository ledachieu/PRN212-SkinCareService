using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<StaffAction> StaffActions { get; set; } = new List<StaffAction>();

    public virtual ICollection<Therapist> Therapists { get; set; } = new List<Therapist>();
}
