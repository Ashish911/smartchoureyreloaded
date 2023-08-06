using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class ChangeDetailLog
{
    public long ChangeDetailLogId { get; set; }

    public string? FkChangeDetailId { get; set; }

    public string? Property { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }
}
