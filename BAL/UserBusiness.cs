using Common.RequestModel;
using Common.ResponseModel;
using DAL;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;

namespace BAL
{
    public class UserBusiness : BaseBusiness
    {
        private readonly UserDataAccess userDataAccess;
        private readonly UserSessionBusiness userSessionBusiness;

        public UserBusiness()
        {
            userDataAccess = new UserDataAccess();
            userSessionBusiness = new UserSessionBusiness();
        }

        public List<UserResponse> GetUserList()
        {
            var userList = userDataAccess.GetUserList();
            var userListResponses = ListMapping<User, UserResponse>(userList);
            return userListResponses;
        }

        public UserResponse GetUser(int UserId)
        {
            User User = userDataAccess.GetUser(UserId);
            UserResponse UserListResponse = ObjectMapping<User, UserResponse>(User);
            return UserListResponse;
        }

        public UserResponse GetUserByAuthenticationToken(string authToken)
        {
            int userId = userSessionBusiness.GetUserIdByAuthToken(authToken);

            if (userId > 0)
            {
                UserResponse user = GetUser(userId);
                return user;
            }
            return null;
        }

        public UserResponse PostUser(UserRequest UserRequest)
        {
            User User = ObjectMapping<UserRequest, User>(UserRequest);
            User = userDataAccess.PostUser(User);
            UserResponse UserResponse = ObjectMapping<User, UserResponse>(User);
            return UserResponse;
        }

        public int PutUser(UserRequest UserRequest)
        {
            User User = ObjectMapping<UserRequest, User>(UserRequest);
            int result = userDataAccess.PutUser(User);
            return result;
        }

        public int DeleteUser(int UserId)
        {
            int result = userDataAccess.DeleteUser(UserId);
            return result;
        }

        public UserResponse AuthenticateUser(LoginRequest loginRequest)
        {
            User user = userDataAccess.AuthenticateUser(loginRequest);
            UserResponse userResponse = ObjectMapping<User, UserResponse>(user);
            return userResponse;
        }


    }
}
