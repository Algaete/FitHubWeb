using System;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Supabase;

namespace FitHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly Supabase.Client _supabase;

        public AuthController(IUserRepository userRepository, IConfiguration configuration, Supabase.Client supabase)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _supabase = supabase;
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Authenticate with Supabase
                var response = await _supabase.Auth.SignIn(request.Email, request.Password);
                
                if (response?.User == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Get user from our database
                var user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null)
                {
                    // Create user in our database if it doesn't exist
                    user = new User
                    {
                        Email = request.Email,
                        FullName = response.User.UserMetadata?.GetValueOrDefault("full_name")?.ToString() ?? request.Email,
                        Role = "user",
                        CreatedAt = DateTime.UtcNow
                    };
                    user = await _userRepository.CreateAsync(user);
                }

                var token = GenerateJwtToken(user);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        id = user.Id,
                        email = user.Email,
                        name = user.FullName,
                        role = user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Register with Supabase
                var response = await _supabase.Auth.SignUp(
                    email: request.Email,
                    password: request.Password,
                    options: new Supabase.Gotrue.SignUpOptions
                    {
                        Data = new Dictionary<string, object>
                        {
                            { "full_name", request.FullName }
                        }
                    }
                );

                if (response?.User == null)
                {
                    return BadRequest(new { message = "Error creating user" });
                }

                // Create user in our database
                var user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Role = "user",
                    CreatedAt = DateTime.UtcNow
                };

                var createdUser = await _userRepository.CreateAsync(user);
                if (createdUser == null)
                {
                    return BadRequest(new { message = "Error creating user" });
                }

                var token = GenerateJwtToken(createdUser);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        id = createdUser.Id,
                        email = createdUser.Email,
                        name = createdUser.FullName,
                        role = createdUser.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating user" });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
} 