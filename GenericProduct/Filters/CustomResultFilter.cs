using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using MigrationToolApi.Controllers;
using Common.ResponseModel;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using BAL;
using Common.ExtendedFunction;
using Common.ProductContant;

namespace MigrationToolApi.Filters
{
    public class CustomResultFilter : ActionFilterAttribute, IResultFilter
    {

        private const string createdBy = "CreatedBy";
        private const string requestedBy = "RequestedBy";


        private const string exceptionMessage = "You don't have the rights to see the data.";
        private int createdByUserId;
        private int requestedByUserId;
        private readonly UserSessionBusiness userSessionBusiness;

        public CustomResultFilter()
        {
            userSessionBusiness = new UserSessionBusiness();
        }
        private string GetToken(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            string token = null;

            request.Headers.TryGetValue(HeaderConstant.TokenHeaderName, out StringValues Headers);

            if (Headers.Count > 0 && !Headers[0].IsNullOrWhitespaceOrEmpty())
                token = Headers[0].Replace("Bearer", "").Trim();

            return token;
        }

        private int GetCurrentUser(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            // Getting token from header
            string token = GetToken(request);

            if (token == null)
                return 0;

            int userId = userSessionBusiness.GetUserIdByAuthToken(token);

            return userId;
        }

        private void VerifyUserAuthentication(ResultExecutingContext context)
        {
            // Get current user from session
            int userId = GetCurrentUser(context.HttpContext.Request);

            // if user, who did request, is different
            if (!(userId > 0 && ((createdByUserId > 0 && userId == createdByUserId ) || (requestedByUserId > 0 &&  userId == requestedByUserId))))
            {
                context.Result = null;
                new Exception(exceptionMessage);
            }
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            ObjectResult result = (ObjectResult)context.Result;

            if (result.StatusCode == (int)HttpStatusCode.OK && result.Value != null)
            {
                string serializedResult = JsonConvert.SerializeObject(result.Value);
                object deserialzedResult = JsonConvert.DeserializeObject(serializedResult);

                if (deserialzedResult != null)
                {
                    string _jsonType = deserialzedResult.GetType().Name;

                    if (_jsonType == JsonConstant.JArray)
                    {
                        JArray resultArray = JArray.FromObject(deserialzedResult);

                        if (resultArray.Count > 0)
                        {
                            JObject allProp = (JObject)resultArray.First;
                            createdByUserId = Convert.ToInt32(allProp.GetValue(createdBy));
                            requestedByUserId = Convert.ToInt32(allProp.GetValue(requestedBy));
                            VerifyUserAuthentication(context);
                        }
                    }
                    else if (_jsonType == JsonConstant.JObject)
                    {
                        var res = JObject.Parse(deserialzedResult.ToString());
                        createdByUserId = Convert.ToInt32(res.GetValue(createdBy));
                        requestedByUserId = Convert.ToInt32(res.GetValue(requestedBy));
                        VerifyUserAuthentication(context);
                    }
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            // Can't add to headers here because response has started.
        }
    }
}
