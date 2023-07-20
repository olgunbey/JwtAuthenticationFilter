using System.Security.Claims;

namespace JwtAuthentication.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; } = null;
        public UserModel()
        {

        }
        public UserModel(IEnumerable<Claim> claims)
        {
            Claims.AddRange(claims);
        }
        
    }
}
