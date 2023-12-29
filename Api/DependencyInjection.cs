using Api.Common.Errors;
using Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPresentation(this IServiceCollection services)
        {
            //services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

            services.AddMappings();

            return services;
        }
    }
}
