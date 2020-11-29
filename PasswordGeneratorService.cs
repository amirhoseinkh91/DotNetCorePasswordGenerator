using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Password.Generator;
using Password.Generator.Options;
using System;

namespace Password.Generatora.Service
{
    public static class PasswordGeneratorService
    {
        public static void AddPasswordGenerator(this IServiceCollection services)
        {
            services.AddSingleton(new PasswordGenerator());
        }

        public static void AddPasswordGenerator(this IServiceCollection services, Action<PasswordGeneratorOptions> configure)
        {
            var options = new PasswordGeneratorOptions();
            configure(options);
            services.AddSingleton(new PasswordGenerator(options));
        }

        public static void AddPasswordGenerator(this IServiceCollection services, Action<PasswordOptions> configure)
        {
            var options = new PasswordOptions();
            configure(options);
            services.AddSingleton(new PasswordGenerator(options));
        }

        public static void AddPasswordGenerator(this IServiceCollection services, PasswordGeneratorOptions options)
        {
            services.AddSingleton(new PasswordGenerator(options));
        }

        public static void AddPasswordGenerator(this IServiceCollection services, PasswordOptions options)
        {
            services.AddSingleton(new PasswordGenerator(options));
        }
    }
}
