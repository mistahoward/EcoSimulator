    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using backend.Models;
    using Microsoft.IdentityModel.Tokens;

    namespace backend.Services
    {
        public class AuthService
        {
            private readonly IConfiguration _config;

            public AuthService(IConfiguration config)
            {
                _config = config;
            }
            public string GenerateJWTToken(User user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            public byte[] GenerateSalt()
            {
                byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                return salt;
            }
            public byte[] HashPassword(string password, byte[] salt)
            {
                using var hmac = new HMACSHA512(salt);

                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return passwordHash;
            }
        }
    }