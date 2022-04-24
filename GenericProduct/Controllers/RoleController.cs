using BAL;
using Common.RequestModel;
using Common.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MigrationToolApi.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleBusiness RoleBusiness;
        public RoleController()
        {
            RoleBusiness = new RoleBusiness();
        }
        // GET: api/Role
        [HttpGet]
        public ActionResult<IEnumerable<RoleResponse>> Get()
        {
            return Ok(RoleBusiness.GetRoleList());
        }

        // GET: api/Role/5
        [HttpGet("{RoleId}", Name = "GetRoleById")]
        public ActionResult<RoleResponse> GetRoleById(int RoleId)
        {
            if (RoleId > 0)
            {
                RoleResponse Role = RoleBusiness.GetRole(RoleId);
                if (Role == null)
                    return NotFound();
                return Ok(Role);
            }
            return BadRequest();
        }

        // POST: api/Role
        [HttpPost]
        public ActionResult<RoleResponse> Post([FromBody] RoleRequest RoleRequest)
        {
            if (RoleRequest != null)
            {
                RoleResponse RoleResponse = RoleBusiness.PostRole(RoleRequest);
                return Ok(RoleResponse);
            }
            return BadRequest();
        }

        // PUT: api/Role/5
        [HttpPut("{RoleId}")]
        public ActionResult<int> Put(int RoleId, [FromBody] RoleRequest RoleRequest)
        {
            if (RoleId == 0 || RoleId != RoleRequest.RoleId)
                return BadRequest();

            return Ok(RoleBusiness.PutRole(RoleRequest));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{RoleId}")]
        public ActionResult<int> Delete(int RoleId = 0)
        {
            if (RoleId == 0)
                return BadRequest();
            else
                return Ok(RoleBusiness.DeleteRole(RoleId));
        }
    }
}
