using System;
using Microsoft.Extensions.DependencyInjection;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Tools
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var homeOwner = serviceProvider.GetRequiredService<BaseService<HomeOwenerEntity, DTOHomeOwener>>();
            homeOwner.AddRecord(new DTOHomeOwener
            {
                title = "محمد مهرنیا",
                PhoneNumber = "09163085306"
            });
        }
    }
}