using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace OrderManagement.API.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var modelErrors = context.ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    });

                    var payload = new
                    {
                        Message = "Incorrect request data",
                        Details = modelErrors.Select(x => new { x.Field, x.Errors })
                        .ToArray()
                    };

                    return new BadRequestObjectResult(payload)
                    {
                        ContentTypes = { "application/json" }
                    };

                };
            });

            return services;
        }
    }
}
