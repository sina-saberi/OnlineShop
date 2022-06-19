using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Interfaces;

namespace Shop.API.CustomAttributes
{
    public class AccessControllAttribute : ActionFilterAttribute
    {
        private readonly string permission;
        public AccessControllAttribute(string permission)
        {
            this.permission = permission;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
            var userId = context.HttpContext.User.Claims.FirstOrDefault(u => u.Type == "userId")!.Value;

            if (!await permissionService!.CheckPermission(Guid.Parse(userId), permission))
            {
                context.Result = new UnauthorizedObjectResult("access denied");
            }
            else
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
