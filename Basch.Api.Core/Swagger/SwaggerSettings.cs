using Microsoft.OpenApi.Models;

namespace Basch.Api.Core.Swagger
{
    public class SwaggerSettings
    {
        public SwaggerSettings()
        {
            Name = "CactuarApi";
            Info = new OpenApiInfo
            {
                Title = "CactuarApi",
                Description = "Resource API for https://penelo.me."
            };
        }

        public string Name { get; set; }
        public OpenApiInfo Info { get; set; }
    }
}