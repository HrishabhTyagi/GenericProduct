using Common.RequestModel;
using DAL;
using SqlServerEntity.EntityModel;

namespace BAL
{
    public class UserSessionBusiness : BaseBusiness
    {
        private readonly UserSessionDataAccess userSessionDataAccess;
        public UserSessionBusiness()
        {
            userSessionDataAccess = new UserSessionDataAccess();
        }

        public bool PutSession(string authToken)
        {
            return userSessionDataAccess.PutSession(authToken);
        }

        public bool PostSession(UserSessionTrackingRequest userSessionTrackingRequest)
        {
            UserSessionTracking userSessionTracking = ObjectMapping<UserSessionTrackingRequest, UserSessionTracking>(userSessionTrackingRequest);
            int result = userSessionDataAccess.PostSession(userSessionTracking);

            return result > 0;
        }

        /// <summary>
        /// Get UserId by Authentication token
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public int GetUserIdByAuthToken(string authenticationToken)
        {
            return userSessionDataAccess.GetUserIdByAuthToken(authenticationToken);
        }

        public bool DeactivateAuthToken(string authToken)
        {
            return userSessionDataAccess.DeactivateAuthToken(authToken);
        }
    }
}
