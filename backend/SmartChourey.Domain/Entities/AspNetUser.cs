﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AspNetUser: IdentityUser
{
    public string Id { get; set; } = null!;

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEndDateUtc { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string UserName { get; set; } = null!;

    public string? NormalizedUserName { get; set; }

    public string? NormalizedEmail { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();
}
