using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using LogProxy.Application.CQRS.Auth.Queries;
using LogProxy.Api.Filters;

namespace LogProxy.Api
{
    public static class StartupFluentValidation
    {
        public static void ConfigureFluentValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetAuthenticationTokenQueryValidator>());
        }
    }
}
