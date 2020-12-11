using Microsoft.OpenApi.Models;

namespace Basch.Api.Core.Swagger
{
    public class SwaggerSettings
    {
        public SwaggerSettings()
        {
            Name = "Basch";
            Info = new OpenApiInfo
            {
                Title = "Basch",
                Description = "Authentication API."
            };
        }

        public string Name { get; set; }
        public OpenApiInfo Info { get; set; }
    }
}