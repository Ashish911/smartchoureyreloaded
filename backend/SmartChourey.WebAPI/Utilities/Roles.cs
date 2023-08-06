using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartChourey.WebAPI.Utilities
{
    public static class Roles
    {
        public const string MainAdmin = "MainAdmin";
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string SubAdmin = "SubAdmin";
        public const string User = "User";
        public const string WebUser = "Web-User"; // Not Logged in user

    }
}