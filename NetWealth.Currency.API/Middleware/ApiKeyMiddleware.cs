using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using QUBE.Web.API.Services;

namespace NetWealth.Currency.API.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
     

        public async Task Invoke(HttpContext context, IUserService userService, IMapper  mapper/*, IJwtUtils jwtUtils*/)
        {

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Api Key was not provided");
                return;
            }


            var apiUser = await userService.GetValidUserByKey(extractedApiKey);
            if (apiUser == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Unauthorized client. Invalid Api Key");
                return;
            }

            //TODO: A

            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var userId = jwtUtils.ValidateJwtToken(token);

            //if(token != null && userId != apiUser.UserId)
            //{
            //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    await context.Response.WriteAsync("Unauthorized client. Invalid Api Key for client");
            //    return;
            //}

            //if (userId != null)
            //{
            //        var user = await userService.GetValidUserById(userId.GetValueOrDefault(0));
            //        if (user == null)
            //        {
            //            context.Response.StatusCode = StatusCodes.Status404NotFound; 
            //            await context.Response.WriteAsync("Unauthorized client. Not Found");

            //        return;
            //        }

            //        var mappedUser = mapper.Map<User>(user);
            //        context.Items["User"] = mappedUser;
            //}

            await _next(context);
        }
    }
}