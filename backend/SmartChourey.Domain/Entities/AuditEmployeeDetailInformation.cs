using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditEmployeeDetailInformation
{
    public long AuditEmployeeId { get; set; }

    public long? EmployeeId { get; set; }

    public string UserId { get; set; } = null!;

    public string? FamilyNameRoman { get; set; }

    public string? GivenNameRoman { get; set; }

    public string? FamilyNameChinese { get; set; }

    public string? GivenNameChinese { get; set; }

    public string? FamilyNameKana { get; set; }

    public string? GivenNameKana { get; set; }

    public string Dob { get; set; } = null!;

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public string Country { get; set; } = null!;

    public string? Postbox { get; set; }

    public string? Prefecture { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string RoleName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public string Email { get; set; } = null!;

    public string ActionDone { get; set; } = null!;

    public string? PhotoPath { get; set; }

    public string? Photoname { get; set; }
}
