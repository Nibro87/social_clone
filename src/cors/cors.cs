public class CorsMiddleware
{
    private readonly RequestDelegate _next;

    public CorsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var origin = context.Request.Headers["Origin"].ToString();

        // Handle preflight requests
        if (context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrEmpty(origin))
            {
                // Set CORS headers
                context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
                context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, HEAD";
                context.Response.Headers["Access-Control-Allow-Headers"] = "Origin, Accept, Content-Type, Authorization, x-access-token";
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";

                context.Response.StatusCode = StatusCodes.Status200OK;
                return;
            }
        }

        // Set CORS headers for non-preflight requests
        if (!string.IsNullOrEmpty(origin))
        {
            context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
            context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, HEAD";
            context.Response.Headers["Access-Control-Allow-Headers"] = "Origin, Accept, Content-Type, Authorization, x-access-token";
            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        }

        await _next(context);
    }
}


