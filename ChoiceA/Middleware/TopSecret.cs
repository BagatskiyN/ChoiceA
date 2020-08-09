using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Middleware
{
    public class TopSecret
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly string _path;

        public TopSecret(RequestDelegate next, IWebHostEnvironment env, string path)
        {
            _next = next;
            _env = env;
            _path = path;
        }

      
        public async Task Invoke(HttpContext context)
        {
            var name = context.Request.Path.Value.Split("/").Last();     
            if (name.EndsWith(".secret"))
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    Array userData = File.ReadAllLines("wwwroot/secret.secret");
                    await context.Response.WriteAsync("Access to file secret granted ");
                
                    foreach (string line in userData)
                    {
                     
                     await context.Response.WriteAsync(line);
                   
                       
                    }
                 
                }


                else
                {
                    await context.Response.WriteAsync("User is not authenticated");

                }
            }

            else
            {
                await _next(context);
            }

        }
    }
    static class TopSecretExtentions
    {
        public static IApplicationBuilder UseTopSecret(this IApplicationBuilder app, string path)
        {
            return app.UseMiddleware<TopSecret>(path);
        }
    }

}
