using Microsoft.Extensions.DependencyInjection;
using LogProxy.Application.Infrastructure.AutoMapper;
using System.Reflection;

namespace LogProxy.Api
{
    public static class StartupAutoMapper
    {
        public static void ConfigureAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
        }
    }
}
