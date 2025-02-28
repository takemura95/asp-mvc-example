using Clerk.BackendAPI.Helpers.Jwks;

namespace clerk_asp_mvc_example.Middlewares;

public class ClerkAuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var options = new AuthenticateRequestOptions(
            secretKey: Environment.GetEnvironmentVariable("CLERK_SECRET_KEY"),
            authorizedParties: ["http://localhost:5173"]
        );

        var requestStateRequest = await AuthenticateRequest.AuthenticateRequestAsync(context.Request, options);

        if (requestStateRequest.IsSignedIn())
        {
            if (requestStateRequest.Claims != null){
                context.User = requestStateRequest.Claims;
            }
            context.Items["RequestState"] = requestStateRequest;

        }

        await next(context);
    }
}