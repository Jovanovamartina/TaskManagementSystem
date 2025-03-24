

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly Dictionary<string, string> _twoFactorCodes = new Dictionary<string, string>(); // In-memory storage for simplicity



        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;

        }

        // Login method
        public async Task<string> Authenticate(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // After login success, generate and send 2FA code
            await SendTwoFactorCode(loginDto.Username);

            return "2FA code sent to email"; // Inform user to check email
        }

        // Method to send 2FA code via email
        public async Task<bool> SendTwoFactorCode(string email)
        {
            var code = new Random().Next(100000, 999999).ToString(); // Generate a 6-digit random code

            // Save the code temporarily (in a real app, use a database or cache with expiration time)
            _twoFactorCodes[email] = code;

            // Send the code via email
            var message = new SendGridMessage
            {
                From = new EmailAddress("your-email@example.com", "YourApp"),
                Subject = "Your 2FA Code",
                PlainTextContent = $"Your 2FA code is: {code}",
                HtmlContent = $"<strong>Your 2FA code is: {code}</strong>"
            };
            message.AddTo(new EmailAddress(email));

            var client = new SendGridClient("your-sendgrid-api-key"); // Replace with your SendGrid API Key
            var response = await client.SendEmailAsync(message);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        // Method to verify the 2FA code entered by the user
        public async Task<bool> VerifyTwoFactorCode(string email, string code)
        {
            if (_twoFactorCodes.ContainsKey(email) && _twoFactorCodes[email] == code)
            {
                // Code is valid, generate JWT
                var user = await _userManager.FindByEmailAsync(email);
                return GenerateJwtToken(user); // Generate and return a token
            }

            return false; // Invalid code
        }

        private bool GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string jwtToken = tokenHandler.WriteToken(token);

            return true; // Return success after generating the token
        }

        public async Task<ApplicationUser> RegisterUserAsync(RegisterDto registerDto)
        {
            {
                if (registerDto == null || string.IsNullOrWhiteSpace(registerDto.Password))
                {
                    throw new ArgumentException("User details and password cannot be empty.");
                }

                var user = new ApplicationUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

                // Creating the user and storing the result of the operation
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                // Now check if result.Succeeded is true
                if (result.Succeeded)
                {
                    // Default role assignment (e.g., "User")
                    var role = "User"; // Default role for all users

                    // Check for special condition to make the user an Admin
                    if (registerDto.Email.EndsWith("@admin.com")) // Example condition (could be different)
                    {
                        role = "Admin"; // Assign Admin role for specific conditions
                    }

                    // Ensure role exists
                    var roleExist = await _roleManager.RoleExistsAsync(role);
                    if (roleExist)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        throw new ArgumentException("Role does not exist.");
                    }
                }
                else
                {
                    // Handle failed registration (result.Errors contains the errors)
                    throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                return user;
            }

        }
    }
}