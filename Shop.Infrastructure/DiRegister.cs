using Microsoft.Extensions.DependencyInjection;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Interfaces;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Utility;

namespace Shop.Infrastructure
{
    public static class DiRegister
    {
        public static void AddInfrastructureDi(this IServiceCollection Services)
        {
            Services.AddSingleton<EncriptionUtility>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddSingleton<FileUtility>();
        }
    }
}
