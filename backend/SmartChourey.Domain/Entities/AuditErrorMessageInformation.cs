using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditErrorMessageInformation
{
    public long AuditErrorId { get; set; }

    public long ErrorId { get; set; }

    public string? FileName { get; set; }

    public string? MethodName { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? OperationDoneBy { get; set; }

    public bool? Status { get; set; }

    public DateTime? SolvedDate { get; set; }

    public string ActionDone { get; set; } = null!;
}
