using JwtAuthentication.Models;

namespace JwtAuthentication.JwtService
{
    public interface IService
    {
        Task<TokenResponse> GenerateToken(string username); //token oluştur
        string RefreshToken(); //accestoken kullan
    }
}
