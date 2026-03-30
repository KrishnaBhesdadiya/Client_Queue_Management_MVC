using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Frontend_Exam.Services
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("JWTToken") == null)
            {
                filterContext.Result = new RedirectResult("~/auth/login");
            }
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Session.GetString("JWTToken") == null)
            {
                context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                context.HttpContext.Response.Headers["Expires"] = "-1";
                context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            }
            base.OnResultExecuted(context);
        }
    }
}
