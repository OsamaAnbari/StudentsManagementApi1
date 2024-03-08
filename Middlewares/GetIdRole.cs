using System.Security.Claims;

namespace Students_Management_Api.Middlewares
{
    public class GetIdRole
    {
        private readonly RequestDelegate _next;

        public GetIdRole(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var NameClaim1 = context.User.FindFirst(ClaimTypes.NameIdentifier);
            var NameClaim2 = context.User.FindFirst(ClaimTypes.Role);
            
            if (NameClaim1 != null)
            {
                context.Items["userId"] = NameClaim1.Value;
                context.Items["role"] = NameClaim2.Value;
            }

            await _next(context);
        }
    }
}