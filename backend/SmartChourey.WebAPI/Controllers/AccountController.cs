using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.WebAPI.Models;
using SmartChourey.WebAPI.Utilities;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly IEmailHelpers _emailHelpers;
        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IConfiguration configuration, 
            IEmployeeService employeeService,
            IEmailHelpers emailHelpers)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _employeeService = employeeService;
            _emailHelpers = emailHelpers;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var roleItems = await _userManager.GetRolesAsync(user);
                    var roleName = roleItems.FirstOrDefault();

                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, roleName)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Secret")));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials);

                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    HttpContext.Session.SetString("userId", user.Id);
                    HttpContext.Session.SetString("userName", user.UserName);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        userId = user.Id,
                        userName = user.UserName
                    });
                }
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] EmployeeDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid request" });
            }

            try
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email, LockoutEnabled = false };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Save Role
                    var AddUserToRole = await _userManager.AddToRoleAsync(user, Roles.User);

                    //Save to Employee table too
                    model.UserId = user.Id;
                    model.IsActive = true;
                    model.DOB = new DateTimeHelper().GetTokyoDate().ToShortDateString();
                    model.Country = "Japan";
                    model.RoleName = Utilities.Roles.User;

                    // insert into Employee Table
                    var resultEmp = await _employeeService.InsertAsync(model);

                    if (!resultEmp.IsSuccessful)
                    {
                        // if failed to insert on Employee Table then Delete the user and role
                        var findUserById = await _userManager.FindByIdAsync(user.Id);
                        var rolesForUser = await _userManager.GetRolesAsync(user);
                        if (rolesForUser.Count() > 0)
                        {
                            foreach (var item in rolesForUser.ToList())
                            {
                                // item should be the name of the role
                                var resultValue = await _userManager.RemoveFromRoleAsync(user, item);
                            }
                        }
                        await _userManager.DeleteAsync(user);

                        return BadRequest(new { Message = resultEmp.Message });
                    }

                    return Ok(new { Message = resultEmp.Message });
                }
                else
                {
                    return BadRequest(new { Message = "Email Exists" });
                }
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getProfile")]
        public async Task<EmployeeDetailViewModel> GetProfile(string id)
        {
            return await _employeeService.GetProfile(id);
        }

        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] EmployeeDetailViewModel model, string dob)
        {
            var resultEmp = await _employeeService.UpdateAsync(model);

            if (resultEmp.IsSuccessful)
            {
                return Ok(new { Message = resultEmp.Message });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (changePasswordResult.Succeeded)
                    {
                        // Password changed successfully
                        return Ok();
                    }
                    else
                    {
                        // Failed to change the password
                        return BadRequest(changePasswordResult.Errors);
                    }
                }
                else
                {
                    // Invalid email or current password
                    return BadRequest("Invalid email or current password.");
                }
            }

            return BadRequest("Invalid request.");
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> forgotPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                    if (user == null)
                    {
                        return StatusCode(404, "User Not Found");
                    }

                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var param = new Dictionary<string, string?>
                    {
                        {"token", token },
                        {"email", forgotPasswordViewModel.Email }
                    };

                    var callbackUrl = QueryHelpers.AddQueryString(forgotPasswordViewModel.ClientURI, param);

                    var isSuccess = await _emailHelpers.SendForgotPasswordEmail(forgotPasswordViewModel.Email, callbackUrl);

                    return StatusCode(200, isSuccess);

                }

                return StatusCode(400, ModelState.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Handle user not found scenario
                    return BadRequest("Invalid user or token");
                }

                var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.ConfirmPassword);
                if (resetPassResult.Succeeded) 
                {
                    // Password reset successful, return success response
                    return Ok(new { message = "Password reset successful." });
                }
                else
                {
                    var errors = resetPassResult.Errors.Select(e => e.Description);
                    // Password reset failed, return error response
                    return BadRequest(new { Errors = errors });
                }
            }

            // If we got this far, something failed, return an error response
            return BadRequest(ModelState);
        }

    }
}