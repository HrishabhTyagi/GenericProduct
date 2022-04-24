using BAL;
using Common.ExtendedFunction;
using Common.ProductContant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace MigrationToolApi.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        private UserSessionBusiness userSessionBusiness;
        public CustomActionFilter()
        {
            userSessionBusiness = new UserSessionBusiness();
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        private string GetTokenFromHeader(IHeaderDictionary headerDictionary)
        {
            string token = null;

            headerDictionary.TryGetValue(HeaderConstant.TokenHeaderName, out StringValues Headers);

            if (Headers.Count > 0 && !Headers[0].IsNullOrWhitespaceOrEmpty())
                token = Headers[0].Replace("Bearer", "").Trim();

            return token;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string authToken = GetTokenFromHeader(context.HttpContext.Request.Headers);
            // Update session in database
            bool isSessionUpdated = userSessionBusiness.PutSession(authToken);

            if (!isSessionUpdated)
                context.Result = new UnauthorizedResult();

            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
