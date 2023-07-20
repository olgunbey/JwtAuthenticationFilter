using Autofac;
using JwtAuthentication.Context;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web;

namespace JwtAuthentication.Filter
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticationFilterAttribute:ActionFilterAttribute
    {
        private readonly IDbService _dbService;
        public AuthenticationFilterAttribute(IDbService dbService)
        {
            _dbService = dbService;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var reader=new StreamReader(context.HttpContext.Request.Body);
            var requestBodyies =await reader.ReadToEndAsync();
            var deger= HttpUtility.UrlDecode(requestBodyies);
            List<string[]> yenidizi=new();
            string[] dizi= deger.Split("&");
            foreach (var item in dizi)
            {
              yenidizi.Add(item.Split("="));
            }
            LoginDto loginDto = new LoginDto()
            {
                KullaniciName = yenidizi[0][1],
                KullaniciSifre= yenidizi[1][1],
                RefreshToken = yenidizi[2][1]
            };
            var kontrolmodels=  context.HttpContext.RequestServices.GetService(typeof(KontrolModels)) as KontrolModels;
            var loginInUsers = await _dbService.LoggedInUsers(x => x.UserName == loginDto.KullaniciName && x.UserPassword == loginDto.KullaniciSifre);
            if (loginInUsers.UserName!=null) //böyle bir kullanıcı giriş yapmış
            {
                if (loginInUsers.RefreshToken == loginDto.RefreshToken && loginInUsers.RefreshTokenTime > DateTime.Now) //refresh token geçerli mi 
                {
                    var JwtSecurityHandler = new JwtSecurityTokenHandler();
                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var Token=jwtSecurityTokenHandler.ReadJwtToken(loginInUsers.AccessToken);


                    foreach (var token in Token.Claims)
                    {
                        if(token.Value == "abc" && token.Type == ClaimTypes.Role)
                        {
                            
                        }
                        else
                        {
                            kontrolmodels.Kontol = true;
                            await _dbService.LoggedUserRemove(loginInUsers);
                            return;
                             //burada vt'den bu kişi silindi
                        }
                        break;
                    }
                }
            }
           var dto= context.HttpContext.RequestServices.GetService(typeof(LoginDto)) as LoginDto;
            dto.KullaniciName = yenidizi[0][1];
            dto.KullaniciSifre = yenidizi[1][1];
            dto.RefreshToken = yenidizi[2][1];
            //böyle bir kullanıcı giriş yapmamış api'ye git giriş yaptır

        }
    }
}
