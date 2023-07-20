using JwtAuthentication.Models;
using System.Linq.Expressions;

namespace JwtAuthentication.Context
{
    public interface IDbService
    {
        Task<bool> UserAny(Expression<Func<Users,bool>> expression);
        Task UserAdd(Users users);
        Task<Users> FirstOrDefault(Expression<Func<Users,bool>> expression);
        Task<LoginInUsers> LoggedInUsers(Expression<Func<LoginInUsers,bool>> expresssion);
        void LoggedUserAdd(LoginInUsers loginInUsers);
        Task LoggedUserRemove(LoginInUsers loginInUsers);
    }
}
