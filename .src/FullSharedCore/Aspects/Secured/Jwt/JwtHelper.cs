using FullSharedCore.Aspects.Secured.Encryption;
using FullSharedCore.Aspects.Secured.Extensions;
using FullSharedCore.Aspects.Secured.Jwt.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Aspects.Secured.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace FullSharedCore.Aspects.Secured.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private TokenOptions _tokenOptions = new TokenOptions();
        private DateTime _accessTokenExpiration;
        private string clientIpAdresi;
        public JwtHelper(TokenOptionsModel _model)
        {
            IActionContextAccessor _httpContextAccessor = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IActionContextAccessor>();
            clientIpAdresi = _httpContextAccessor != null
                ? $"{_httpContextAccessor.ActionContext.HttpContext.Connection.RemoteIpAddress}X{_httpContextAccessor.ActionContext.HttpContext.Connection.RemotePort}"
                : "SecuredKey!ForIpAdress";

            foreach (var item in _model.GetType().GetProperties())
            {
                PropertyInfo propertyInfo = _tokenOptions.GetType().GetProperty(item.Name);
                if (propertyInfo != null)
                    propertyInfo.SetValue(_tokenOptions, Convert.ChangeType(item.GetValue(_model), propertyInfo.PropertyType), null);
            }
        }
        public string CreateToken(UserInfo userInfo, List<string> operationClaimsInfo)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, userInfo, signingCredentials, operationClaimsInfo);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return jwtSecurityTokenHandler.WriteToken(jwt);
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserInfo userInfo, SigningCredentials signingCredentials, List<string> operationClaimsInfo)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
            claims: SetClaims(userInfo, operationClaimsInfo),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(UserInfo userInfo, List<string> operationClaimsInfo)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(userInfo.Id);
            claims.AddEmail(userInfo.Email);
            claims.AddName($"{userInfo.Name} {userInfo.Nick}");
            claims.AddRoles(operationClaimsInfo.ToArray());
            claims.AddIpAdressForUserData(clientIpAdresi);
            return claims;
        }



    }
}
