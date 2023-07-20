namespace JwtAuthentication.Models
{
    public class LoginDto
    {
        public string KullaniciName{ get; set; }
        public string KullaniciSifre { get; set; }
        public string RefreshToken { get; set; }
    }
}
