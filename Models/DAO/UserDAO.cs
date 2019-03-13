using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
            return dbContext.Users.Single(user => user.accountName.Equals(userName));
        }

        public IQueryable<User> GetAllUsers()
        {
            return dbContext.Users;
        }

        public bool CheckLogin(string userName, string password)
        {
            try
            {
                var acc = dbContext.Users.First(x => x.accountName == userName && x.accountPassword == password);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> GetRoleByID(string id)
        {
            List<Role> roles = dbContext.Roles.SqlQuery("SELECT * FROM Roles WHERE id IN (SELECT roleID FROM Credential WHERE groupID IN (SELECT groupID FROM Users WHERE Users.id = @id))", new SqlParameter("@id", id)).ToList();
            List<string> res = new List<string>();
            roles.ForEach(role =>
            {
                res.Add(role.id);
            });
            return res;
        }
    }
}
