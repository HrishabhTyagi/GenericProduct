using BAL;
using Common.RequestModel;
using Common.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MigrationToolApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserBusiness userBusiness;

        public UsersController()
        {
            userBusiness = new UserBusiness();
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserResponse>> GetUser()
        {
            return Ok(userBusiness.GetUserList());
        }

        // GET: api/User/5
        [HttpGet("{userId}")]
        public ActionResult<UserResponse> GetUserById(int userId)
        {
            if (userId > 0)
            {
                UserResponse User = userBusiness.GetUser(userId);
                if (User == null)
                    return NotFound();
                return Ok(User);
            }
            return BadRequest();
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<UserResponse> Post([FromBody] UserRequest userRequest)
        {
            if (userRequest != null)
            {
                UserResponse UserResponse = userBusiness.PostUser(userRequest);
                return Ok(UserResponse);
            }
            return BadRequest();
        }

        // PUT: api/User/5
        [HttpPut("{userId}")]
        public ActionResult<int> Put(int userId, [FromBody] UserRequest userRequest)
        {
            if (userId == 0 || userId != userRequest.UserId)
                return BadRequest();

            return Ok(userBusiness.PutUser(userRequest));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}")]
        public ActionResult<int> Delete(int userId = 0)
        {
            if (userId == 0)
                return BadRequest();
            else
                return Ok(userBusiness.DeleteUser(userId));
        }
    }
}
