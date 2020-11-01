using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using very_easy_test_app.Models;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Tools
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new dbContext(serviceProvider.GetRequiredService<DbContextOptions<dbContext>>());
            context.
        }
    }
}