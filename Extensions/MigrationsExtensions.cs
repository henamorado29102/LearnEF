using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEF.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnEF.Extensions
{
    public static class MigrationsExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

            context.Database.Migrate();
        }
    }
}