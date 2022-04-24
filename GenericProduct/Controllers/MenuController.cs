using BAL;
using Common.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MigrationToolApi.Filters;

namespace MigrationToolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [CustomExceptionFilter]
    [CustomActionFilter]
    public class MenuController : ControllerBase
    {
        private readonly MenuBusiness menuBusiness;
        private BaseController baseController;

        public MenuController() 
        {
            menuBusiness = new MenuBusiness();
        }

        /// <summary>
        ///  Get Menu List 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MenuResponse>> GetMenuList()
        {
            baseController = new BaseController(HttpContext);
            // Get Current user details
            UserResponse user = baseController.GetCurrentUser();
            if (user != null)
                return Ok(menuBusiness.GetMenuList(user.RoleId));

            return BadRequest();
        }
    }
}