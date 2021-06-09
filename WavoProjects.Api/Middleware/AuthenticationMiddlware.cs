using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Helpers;

namespace WavoProjects.Api.Middleware
{
    // Gets the token and assigns the user to the context
    public class AuthenticationMiddlware
    {
        private readonly RequestDelegate m_next;
        private readonly AuthenticationHelper m_jwtHelper;

        public AuthenticationMiddlware(RequestDelegate next, AuthenticationHelper jwtHelper)
        {
            m_next = next;
            m_jwtHelper = jwtHelper;
        }

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContext(httpContext, token);
            }

            return m_next(httpContext);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            User user = await m_jwtHelper.DecodeToken(token);

            if(user != null)
            {
                context.Items["User"] = user;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddlware>();
        }
    }
}
