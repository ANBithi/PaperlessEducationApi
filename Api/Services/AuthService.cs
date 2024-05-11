using System;
using Api.ViewModels;
using Api.Enums;
using Api.IServices;
using Api.Commons;
using Microsoft.Extensions.Configuration;
using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Api.Security;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Services
{
    public class AuthService : IAuthService
    {
        string jwtSecret;
        int jwtLifespan;
        int jwtPartnerAPILifespan;
        private readonly IEntityId _entityId;
        private readonly IDateTime _dateTime;
        private readonly IConfiguration _config;


        public AuthService(
            IEntityId entityId,
            IDateTime dateTime,
            IConfiguration config
            )
        {
            this.jwtSecret = config["Jwt:Key"];
            this.jwtLifespan = int.Parse(config["Jwt:InternalAPIExpireInMinute"]);
            this.jwtPartnerAPILifespan = int.Parse(config["Jwt:PartnerAPIExpireInMinute"]);
            this._entityId = entityId;
            this._dateTime = dateTime;
            _config = config;

        }
        public AuthData GetAuthData(UserViewModel user)
        {
            var expirationTime = DateTime.UtcNow.AddMinutes(jwtLifespan);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.FirstName + ' ' + user.LastName),
                    new Claim("UserType", ((int)user.UserType).ToString()),                    
                    new Claim(SecurityConstants.IS_ADMIN, user.UserType == UserTypeEnum.Admin ? "true" : "false")
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType
            };
        }

    }

}

