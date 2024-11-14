using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DataCompressionAPI.Data;
using System.Text.RegularExpressions;

namespace DataCompressionAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        
        // Ensure this key matches the key in Program.cs
        private const string SecretKey = "your-very-secret-key"; 
        private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(SecretKey));

        public AuthService(AppDbContext context)
        {
            _context = context;
        }


        public bool Register(string username, string password, string email, bool isAdmin)
        {
            // Validate email format
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format");
            }

            // Validate password strength
            if (!IsValidPassword(password))
            {
                throw new ArgumentException("Password must be at least 6 characters long and contain both letters and numbers");
            }

            // Check if the user already exists in the database
            if (_context.Users.Any(u => u.Username == username))
            {
                return false;
            }

            // Create a new user
            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email,
                Role = isAdmin ? UserRole.Admin : UserRole.User
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for a basic email pattern
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            // Regular expression for password to be at least 6 characters, containing letters and numbers
            var passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");
            return passwordRegex.IsMatch(password);
        }

        public string Login(string username, string password)
        {
            // Retrieve the user from the database
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // Generate and return JWT token if login is successful
                return GenerateToken(user);
            }

            return null; // Login failed
        }

        public bool ResetPassword(string username, string newPassword)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-app", // Match with ValidIssuer in Program.cs
                audience: "your-app", // Match with ValidAudience in Program.cs
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}