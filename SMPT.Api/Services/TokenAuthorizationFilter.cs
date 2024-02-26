using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;
using SMPT.Api.Services.Interface;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMPT.Api.Services
{
    public class TokenAuthorizationFilter : Service<object>, IAuthorizationFilter
    {
        public TokenAuthorizationFilter(
            ILoggerFactory loggerFactory,
            IConfiguration config,
            IUnitOfWork unitOfWork)
            : base(loggerFactory, config, unitOfWork) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("JWT").Get<Jwt>()!.Key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var value = jwtToken.Claims.First(x => x.Type == "userId").Value;
                var userId = Guid.Parse(value);

                // You can further validate the user, for example, check if the user exists in your system, check roles, etc.
                // For simplicity, let's just set a claim indicating the user ID.
                //context.HttpContext.User.AddIdentity(new ClaimsIdentity(new[]
                //{
                //    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                //}));


            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }
}
