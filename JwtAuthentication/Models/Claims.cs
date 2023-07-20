namespace JwtAuthentication.Models
{
    public class Claims
    {
        public int ID { get; set; }
        public string ClaimsName { get; set; }
        public ICollection<Users> Users{ get; set; }
        public ICollection<LoginInUsers> LoginInUsers { get; set; }
    }
}
