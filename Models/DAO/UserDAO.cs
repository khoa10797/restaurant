using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        public string Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user.id;
        }

        public string Remove(string userId)
        {
            User user = new User();
            user.id = userId;
            dbContext.Entry(user).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return userId;
        }

        public bool Login(string userName, string password)
        {
            var res = dbContext.Users.Count(user => user.accountName.Equals(userName) && user.accountPassword.Equals(password));
            return res > 0;
        }

        public User GetByUserName(string userName)
        {
            return dbContext.Users.SingleOrDefault(user => user.accountName.Equals(userName));
        }

        public IQueryable<User> GetAllUsers()
        {
            return dbContext.Users;
        }
    }
}
