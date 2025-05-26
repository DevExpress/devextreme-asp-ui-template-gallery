using DevExtremeVSTemplateMVC.Services;

namespace DevExtremeVSTemplateMVC.Middleware
{
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        const string SESSION_KEEP_FLAG = "keep";

        public RequestContextMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRequestContext requestContext) {
            context.Session.SetString(SESSION_KEEP_FLAG, "true");
            requestContext.SessionId = context.Session.Id;
            await _next(context);
        }
    }
}
