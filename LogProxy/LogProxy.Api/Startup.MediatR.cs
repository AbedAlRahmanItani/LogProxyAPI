using MediatR;
using Microsoft.Extensions.DependencyInjection;
using LogProxy.Application.CQRS.Auth.Queries;
using LogProxy.Application.Infrastructure;
using System.Reflection;

namespace LogProxy.Api
{
    public static class StartupMediatR
    {
        public static void ConfigureMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(GetAuthenticationTokenQueryHandler).GetTypeInfo().Assembly);
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}