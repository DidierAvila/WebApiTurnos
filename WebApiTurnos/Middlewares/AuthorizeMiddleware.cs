using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace WebApiTurnos.Middlewares
{
    public class AuthorizeMiddleware : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public AuthorizeMiddleware()
        {
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var resource = context.Request.RouteValues["controller"].ToString();
                var path = context.Request.Path.Value;

                context.Response.StatusCode = 403;
                return;
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
