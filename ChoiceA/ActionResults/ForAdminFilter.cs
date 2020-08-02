using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.ActionResults
{
    public class ForAdminFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole("admin"))
            {
                context.Result = new ForbidResult();
            }
        }
        public class ForAdminAttribute : Attribute, IFilterFactory
        {
            public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
                new ForAdminFilter();

            public bool IsReusable => false;
        }
        public class ForStudentFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Claims.Any(c => c.Type == "studentId"))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
