using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using StudyAPI.Infra.CrossCutting.Models;


namespace StudyAPI.Infra.CrossCutting.Security
{
    public class TokenBase
    {

        public static TokenModel ReadToken(HttpContext context)
        {
            var token = new TokenModel();

            var hasToken = context.Request.Headers.HasKeys();
            if (hasToken)
            {
                var authHeader = context.Request.Headers["Authorization"];
                var authBits = authHeader.Split(' ');
                if (authBits.Length == 2)
                {
                    try
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var tokenRead = handler.ReadToken(string.Format(authBits[1])) as JwtSecurityToken;

                        var decryptedVolId = SecurityBase.Decrypt(tokenRead.Claims.First(claim => claim.Type == "vId").Value);
                        var decryptedOrgId = SecurityBase.Decrypt(tokenRead.Claims.First(claim => claim.Type == "oId").Value);
                        var decryptedEmail = SecurityBase.Decrypt(tokenRead.Claims.First(claim => claim.Type == "email").Value);

                        int.TryParse(decryptedVolId, out var vId);
                        int.TryParse(decryptedOrgId, out var oId);
                        DateTime.TryParse(tokenRead.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Iat).Value, out var cDate);

                        var tokenId = tokenRead.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

                        token.TokenId = new Guid(tokenId);
                        token.CreateDate = cDate == null ? DateTime.MinValue : cDate;
                        token.VolunteerId = vId;
                        token.OrganisationId = oId;
                        token.Email = decryptedEmail;
                        token.Token = string.Format(authBits[1]);
                        token.IsFormatValid = true;
                    }
                    catch (Exception e)
                    {
                        return token;
                    }
                }
            }

            return token;
        }

        


    }
}
