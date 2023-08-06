﻿using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditQrcodeInformation
{
    public long AuditQrid { get; set; }

    public long Qrid { get; set; }

    public string QrcodeValue { get; set; } = null!;

    public string SiteId { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string ActionDone { get; set; } = null!;
}
