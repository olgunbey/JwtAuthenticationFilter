namespace JwtAuthentication.Models
{
    public class TokenResponse
    {
        public string AccessToken{ get; set; }
        public string RefreshToken{ get; set; }
        public DateTime AccessTokenTime{ get; set; }
        public DateTime RefreshTokenTime { get; set; }
    }
}
