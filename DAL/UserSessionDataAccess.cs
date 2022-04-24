using SqlServerEntity.EntityModel;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL
{
    public class UserSessionDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Updating user session
        /// </summary>
        /// <param name="authToken"></param>
        public bool PutSession(string authToken)
        {
            UserSessionTracking session = _context.UserSessionTrackings.ToList().LastOrDefault(a => a.AuthenticationToken == authToken);

            if (session != null)
            {
                TimeSpan timeDiff = DateTime.Now - session.TokenTime;

                if (timeDiff.TotalMinutes <= 20)
                {
                    session.TokenTime = DateTime.Now;
                    _context.Entry(session).State = EntityState.Modified;
                    int result = _context.SaveChanges();
                    if (result > 0)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get Session details by authentication token
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public int GetUserIdByAuthToken(string authToken)
        {
            UserSessionTracking session = _context.UserSessionTrackings.FirstOrDefault(a => a.AuthenticationToken == authToken);

            if (session != null)
                return session.UserId;
            return 0;
        }

        private bool CheckTimeDiff(DateTime sesionTime)
        {
            TimeSpan timeDiff = DateTime.Now - sesionTime;

            return timeDiff.TotalMinutes <= 20;
        }

        /// <summary>
        /// Check a particular user is logged in more than browser or machine
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void IsSessionExistForUser(int userId)
        {
            List<UserSessionTracking> session = _context.UserSessionTrackings.Where(b => b.IsActive == true && b.UserId == userId).AsEnumerable().Where(a =>
            {
                return CheckTimeDiff(a.TokenTime);
            }).ToList();

            session.ForEach(c => c.IsActive = false);
        }

        /// <summary>
        /// 
        /// Save Authentication Token
        /// </summary>
        /// <param name="userSessionTracking"></param>
        /// <returns></returns>
        public int PostSession(UserSessionTracking userSessionTracking)
        {
            IsSessionExistForUser(userSessionTracking.UserId);
            _context.UserSessionTrackings.Add(userSessionTracking);

            return _context.SaveChanges();
        }

        public bool DeactivateAuthToken(string authToken)
        {
            UserSessionTracking userSessionTracking = _context.UserSessionTrackings.FirstOrDefault(a => a.AuthenticationToken == authToken);

            if (userSessionTracking != null && userSessionTracking.IsActive == true)
                userSessionTracking.IsActive = false;

            int result = _context.SaveChanges();
            return result > 0;
        }
    }
}
