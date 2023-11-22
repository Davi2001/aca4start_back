using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Logger
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public string? MethodName { get; set; }

    public DateTime? DateTimeLog { get; set; }

    public int? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? InnerExcept { get; set; }
}
