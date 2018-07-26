using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using StudyAPI.Business.Interfaces;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.CrossCutting.Security;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace StudyAPI.Business.Business
{
    public class TokenManager : ITokenManager
    {
        private readonly IVolunteerTokenRepository _volunteerTokenRepository;

        public TokenManager(IVolunteerTokenRepository volunteerTokenRepository)
        {
            _volunteerTokenRepository = volunteerTokenRepository;
        }

        public string GenerateToken(GenerateTokenModel model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim("vId", SecurityBase.Encrypt(model.VolunterId.ToString())),
                new Claim("oId", SecurityBase.Encrypt(model.OrganisationId.ToString())),
                new Claim(JwtRegisteredClaimNames.Email, SecurityBase.Encrypt(model.Email))
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature, "http://www.w3.org/2001/04/xmlenc#sha256");

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: model.Issuer,
                audience: model.Audience,
                claims: claims,
                //expires: DateTime.UtcNow,//DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
            );

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var tokenId = claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

            var volunteerToken = new VolunteerToken
            {
                Id = new Guid(tokenId),
                Volunteer_ID = model.VolunterId,
                Token_VC = jwtSecurityTokenHandler,
                Active_BT = true
            };

            _volunteerTokenRepository.SaveToken(volunteerToken);

            return jwtSecurityTokenHandler;
        }

        public bool MakeTokenInvalid(Guid tokenId)
        {
            return  _volunteerTokenRepository.InactiveToken(tokenId);
        }

        public bool DeleteAllVolunteerTokens(int volunteerId)
        {
            return _volunteerTokenRepository.DeleteAllVolunteerTokens(volunteerId);
        }

    }
}
