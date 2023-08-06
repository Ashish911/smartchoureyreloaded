using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AspNetRole: IdentityRole
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<AspNetUser> Users { get; } = new List<AspNetUser>();
}
