using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Framework.Message.Concrete;

namespace Framework.Filter.Concrete
{
    public class ApplicationRequestFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ApplicationRequest actionRequest = context.ActionArguments
                                                      .Select(it => it.Value as ApplicationRequest)
                                                      .FirstOrDefault();

            string token = context.HttpContext.Request.Headers["Authorization"];

            if (actionRequest != null && !string.IsNullOrEmpty(token)) 
            {
                token = token.Replace("Bearer ", string.Empty);
                actionRequest.HandleToken(token);
            }
        }
    }
}
