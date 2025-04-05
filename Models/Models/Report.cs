using System;
using System.Collections.Generic;

namespace SkinCareService.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string ReportType { get; set; } = null!;

    public string DataContent { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
