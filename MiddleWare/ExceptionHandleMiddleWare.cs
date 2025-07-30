using System.Net;

namespace WalksAPI.MiddleWare
{
    public class ExceptionHandleMiddleWare
    {
        private readonly ILogger<ExceptionHandleMiddleWare> logger;
        private readonly RequestDelegate next;

        public ExceptionHandleMiddleWare(ILogger<ExceptionHandleMiddleWare> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex, $"{errorId} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Id = errorId,
                    Message = "An unexpected error occurred. Please try again later.",
                };
                
                    await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
