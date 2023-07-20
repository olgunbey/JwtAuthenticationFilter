using JwtAuthentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JwtAuthentication.Context
{
    public class DbService : IDbService
    {
        protected readonly AppDbContext _appDbContext;
        public DbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Users> FirstOrDefault(Expression<Func<Users, bool>> expression)
        {
           return await _appDbContext.Users.Include(x=>x.Claims).FirstOrDefaultAsync(expression);
        }

        public  Task<LoginInUsers> LoggedInUsers(Expression<Func<LoginInUsers, bool>> expresssion)
        {
            var loginInUser =  _appDbContext.LoginInUsers.Include(x=>x.Claims).FirstOrDefault(expresssion);
            if (loginInUser is null)
            {
                return Task.FromResult(new LoginInUsers());
            }
            return Task.FromResult(loginInUser);
        }

        public void LoggedUserAdd(LoginInUsers loginInUsers)
        {
            _appDbContext.LoginInUsers.Add(loginInUsers);
            _appDbContext.SaveChanges();
        }

        public async Task LoggedUserRemove(LoginInUsers loginInUsers)
        {
            _appDbContext.LoginInUsers.Remove(loginInUsers);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UserAdd(Users users)
        {
            await _appDbContext.Users.AddAsync(users);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> UserAny(Expression<Func<Users, bool>> expression)
        {
           return await _appDbContext.Users.AnyAsync(expression);
        }
    }
}
