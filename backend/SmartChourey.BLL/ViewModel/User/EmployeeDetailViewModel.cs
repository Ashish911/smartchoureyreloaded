using System.ComponentModel.DataAnnotations;

namespace SmartChourey.BLL.ViewModel.User
{
    public class EmployeeDetailViewModel : UserStatusViewModel
    {
        public long? Id { get; set; }
        public string? UserId { get; set; }
        public string? FamilyName_Roman { get; set; }
        public string? GivenName_Roman { get; set; }
        public string? FamilyName_Chinese { get; set; }
        public string? GivenName_Chinese { get; set; }
        public string? FamilyName_Kana { get; set; }
        public string? GivenName_Kana { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? phoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? Country { get; set; }
        public string? Postbox { get; set; }
        public string? Prefecture { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }

        public string? PhotoPath { get; set; }
        public string? photoname { get; set; }

        public string? SiteJoinedDate { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public long? EmployeeId { get; set; }
        public DateTime? DateOfRegister { get; set; }
        public string? SiteId { get; set; }
        public string? SiteName { get; set; }
        public string? SiteAddress { get; set; }
        public string? SiteCity { get; set; }
        public string? SiteCountry { get; set; }
    }
}
