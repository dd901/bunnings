using System;
using Bunnings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bunnings
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var serviceProvider = ConfigureServices(new ServiceCollection()))
            {
                var csvImportExportService = getService<ICsvImportExportService>(serviceProvider);
                var transformDataService = getService<ITransformDataService>(serviceProvider);
                var verificationService = getService<IVerificationService>(serviceProvider);

                var app = new App(verificationService, csvImportExportService, transformDataService);
                app.Run(args);
            }
        }

        private static T getService<T>(ServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<T>() ?? throw new ArgumentNullException();
        }

        private static ServiceProvider ConfigureServices(ServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<ICsvImportExportService, CsvImportExportService>()
                .AddScoped<ITransformDataService, TransformDataService>()
                .AddScoped<IVerificationService, VerificationService>()
                .BuildServiceProvider();
        }
    }
}