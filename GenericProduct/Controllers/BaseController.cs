using Common.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MigrationToolApi.Filters;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BAL;
using Common.ExtendedFunction;
using Common.ProductContant;

namespace MigrationToolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    [CustomExceptionFilter]
    [CustomResultFilter]
    [CustomActionFilter]
    public class BaseController : ControllerBase
    {
        private readonly UserBusiness userBusiness = new UserBusiness();
        public HttpContext httpContext;

        public BaseController()
        {
        }

        public BaseController(HttpContext http)
        {
            httpContext = http;
        }


        /// <summary>
        /// Used for taken out token from request header
        /// </summary>
        /// <returns></returns>
        internal string GetToken(string header)
        {
            string token = null;

            if (httpContext == null)
                httpContext = HttpContext;

            httpContext.Request.Headers.TryGetValue(header, out StringValues Headers);

            if (Headers.Count > 0 && !Headers[0].IsNullOrWhitespaceOrEmpty())
                token = Headers[0].Replace("Bearer", "").Trim();

            return token;
        }

        internal UserResponse GetCurrentUser()
        {
            // Getting token from header
            string token = GetToken(HeaderConstant.TokenHeaderName);

            if (token == null)
                return null;

            UserResponse user = userBusiness.GetUserByAuthenticationToken(token);

            if (user != null)
                return user;
            return null;
        }
    }
}