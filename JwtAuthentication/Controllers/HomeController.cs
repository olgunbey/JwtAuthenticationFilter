using JwtAuthentication.Context;
using JwtAuthentication.Filter;
using JwtAuthentication.JwtService;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IService _service;
        private readonly KontrolModels _kontrolModels;
        private readonly IDbService dbService;
        private readonly LoginDto _loginDto;

        public HomeController(IService service, KontrolModels kontrolModels, IDbService dbService,LoginDto login)
        {
            _service = service;
            _kontrolModels = kontrolModels;
            this.dbService = dbService;
            _loginDto = login;
        }
        [HttpGet("TokenOlustur")]
        public ActionResult TokenOlustur()
        {
            var headers = HttpContext.Request.Headers;
            return Ok(_service.GenerateToken(headers["name"].ToString()));

        }

        [TypeFilter(typeof(AuthenticationFilterAttribute))]
        [HttpPost("GirisYap")]
        public async Task<ActionResult> GirisYap()
        {

            if (_kontrolModels.Kontol)
            {
                return Ok("claimler değiştirildi tekrar giriş yaptır");
            }
           
           if(await dbService.UserAny(x => x.UserName == _loginDto.KullaniciName && x.UserPassword == _loginDto.KullaniciSifre)) //varmı yok mu 
            {
             var Users=  await dbService.FirstOrDefault(x => x.UserName == _loginDto.KullaniciName && x.UserPassword == _loginDto.KullaniciSifre);
                if(Users.UserName!=null)
                {
                    dbService.LoggedUserAdd(new LoginInUsers()
                    {
                        AccessToken = Users.AccessToken,
                        RefreshToken = Users.RefreshToken,
                        RefreshTokenTime = Users.RefreshTokenTime,
                        UserPassword = Users.UserPassword,
                        ClaimsID = Users.ClaimsID,
                        UserName = Users.UserName,
                    });
                }
            }
            return Ok("Giriş yapıldı");
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            TokenResponse token=await _service.GenerateToken(register.userName);
            Users user=new Users()
            {
                UserName=register.userName,
                UserPassword=register.password,
                AccessToken=token.AccessToken,
                RefreshToken=token.RefreshToken,
                RefreshTokenTime=token.RefreshTokenTime,
                ClaimsID=1
            };
           await dbService.UserAdd(user);
            return Ok(token);
        }
    }
}
