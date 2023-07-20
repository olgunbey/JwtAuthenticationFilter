namespace JwtAuthentication.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenTime { get; set; }
        public string AccessToken { get; set; }
        public Claims Claims { get; set; }
        public int ClaimsID { get; set; }
    }
}
