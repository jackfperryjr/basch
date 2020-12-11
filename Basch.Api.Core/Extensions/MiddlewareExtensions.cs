using Microsoft.AspNetCore.Builder;

namespace Basch.Api.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseSwaggerDocuments(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}