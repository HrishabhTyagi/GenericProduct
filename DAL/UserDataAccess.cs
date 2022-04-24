using Microsoft.EntityFrameworkCore;
using SqlServerEntity.EntityModel;
using Common.RequestModel;

namespace DAL
{
    public class UserDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Fetch all user list
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserList()
        {
            
            List<User> users = cache.GetValue<List<User>>(CacheKeys.User);

            if (users != null && users.Count > 0)
                return users;

            users = _context.Users.Where(user => !user.IsDeleted).AsNoTracking().ToList();
            cache.Add(CacheKeys.User, users);
            return users;
        }

        /// <summary>
        /// Get specific portolio by id
        /// </summary>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            List<User> users = cache.GetValue<List<User>>(CacheKeys.User);

            if (users != null && users.Count > 0)
                return users.FirstOrDefault(user => user.UserId == userId && !user.IsDeleted);

            users = _context.Users.Where(user => !user.IsDeleted).ToList();
            cache.Add(BaseDataAccess.CacheKeys.User, users);
            return _context.Users.FirstOrDefault(user => user.UserId == userId && !user.IsDeleted);
        }

        /// <summary>
        /// Insert new User
        /// </summary>
        /// <returns></returns>
        public User PostUser(User User)
        {
            RemoveCache();
            _context.Users.Add(User);
            _context.SaveChanges();
            return User;
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int PutUser(User User)
        {
            RemoveCache();
            User.ModifiedOn = DateTime.Now;
            _context.Entry(User).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        /// <summary>
        /// Soft delete of User
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int DeleteUser(int UserId)
        {
            RemoveCache();
            User User = _context.Users.Find(UserId);

            if (User == null)
                return 0;

            if (!User.IsDeleted)
                User.IsDeleted = !User.IsDeleted;

            _context.Entry(User).State = EntityState.Modified;
            return _context.SaveChanges();
        }


        /// <summary>
        /// Authenticating the user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public User AuthenticateUser(LoginRequest loginRequest)
        {
            List<User> users = cache.GetValue<List<User>>(CacheKeys.User);

            if (users != null && users.Count > 0)
                return users.FirstOrDefault(a => a.EmailId == loginRequest.EmailId && a.Password == loginRequest.Password);

            return _context.Users.FirstOrDefault(a => a.EmailId == loginRequest.EmailId && a.Password == loginRequest.Password);
        }

        private void RemoveCache()
        {
            cache.Delete(CacheKeys.User);
        }
    }
}
