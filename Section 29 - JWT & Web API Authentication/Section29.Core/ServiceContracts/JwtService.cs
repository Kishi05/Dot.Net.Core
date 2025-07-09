using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Section29.Core.DTO;
using Section29.Core.Identity;
using Section29.Core.ServiceContracts.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Section29.Core.ServiceContracts
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponse CreateJWTToken(ApplicationUser user)
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()), // Subject -> User ID
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), // JET -> Unique ID
                new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()), // Issued at Date and Time of Token Generation
                new Claim(ClaimTypes.NameIdentifier,user.Email), // Email
                new Claim(ClaimTypes.Name,user.PersonName) // Name of the User
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = string.Empty;
            try
            {
                token = handler.WriteToken(tokenGenerator);
            }
            catch (Exception ex)
            {

            }

            return new AuthenticationResponse()
            {
                Email = user.Email,
                PersonName = user.PersonName,
                Expiration = expirationTime,
                Token = token,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
            };

        }

        /// <summary>
        /// Creates Refresh Token (Base 64 string using RandomNumberGenerator)
        /// </summary>
        /// <returns></returns>
        private string GenerateRefreshToken()
        {
            Byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

    }
}
