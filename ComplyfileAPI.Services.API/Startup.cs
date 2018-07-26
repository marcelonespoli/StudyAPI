using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace ComplyfileAPI.Services.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
           
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audience = ConfigurationManager.AppSettings["Audience"];
            var secret = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Key"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        RequireExpirationTime = false
                        //ValidateLifetime = true
                    }
                });

        }
    }
}