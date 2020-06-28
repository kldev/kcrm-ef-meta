using Microsoft.AspNetCore.Builder;

namespace KCrm.Server.Api.Infrastructure {
    public static class AppMiddlewareSetup {
        public static IApplicationBuilder UseAppMiddleware(this IApplicationBuilder builder) {
            builder.UseMiddleware<ErrorHandlingMiddleware> ( );
            return builder;
        }
    }
}
