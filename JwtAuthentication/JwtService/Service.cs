using JwtAuthentication.Context;
using JwtAuthentication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthentication.JwtService
{
    public class Service : IService
    {
        private readonly IDbService _dbService;
        public Service(IDbService dbService)
        {
            _dbService = dbService;
        }
        public async  Task<TokenResponse> GenerateToken(string userName)
        {
                var UserModel= await _dbService.FirstOrDefault(x => x.UserName == userName);
                string keyname = "key1key1key1key1key1key1key1key1key1key1key1key1key1key1";
            List<Claim> claims = new List<Claim>();

            if (UserModel is null)
            {
                var claim2 = new Claim(ClaimTypes.Role, "Admin");
                claims.Add(claim2);
            }
            else
            {
                var claim = new Claim(ClaimTypes.Role, UserModel.Claims.ClaimsName);
                claims.Add(claim);
            }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyname));

                var signInCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                var expirationAccessToken = DateTime.Now.AddDays(15); //15 günlük ömür verdik bu accestoken'a;

                var token=new JwtSecurityToken(
                    issuer:"issuer1",
                    audience:"audience1",
                    claims:claims,
                    expires:expirationAccessToken,
                    signingCredentials:signInCredentials);

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                return new TokenResponse() 
                {
                    AccessToken= jwtSecurityTokenHandler.WriteToken(token),
                    RefreshToken=RefreshToken(),
                    AccessTokenTime=expirationAccessToken,
                    RefreshTokenTime=DateTime.Now.AddDays(25) 
                };
            

            
        }

        public string RefreshToken()
        {
            return Guid.NewGuid().ToString();
        }


       

    }
}
