using AutoMapper.Configuration;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.DependencyInjection;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.BuilderExtensions
{
    public static class AddTransientExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services
                .AddTransient<IService<HomeOwenerEntity, DTOHomeOwener>, BaseService<HomeOwenerEntity, DTOHomeOwener>
                >();

            services
                .AddTransient<IService<HomeEntity, DTOHome>, BaseService<HomeEntity, DTOHome>>();
            return services;
        }
    }
}