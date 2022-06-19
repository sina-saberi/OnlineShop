using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application
{
    public static class DiRegister
    {
        public static void AddApplicationDi(this IServiceCollection Services)
        {
            Services.AddScoped<IPermissionService, PermissionService>();
            Services.AddScoped<IProductService, ProductService>();


            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });
            var mapper = config.CreateMapper();
            Services.AddSingleton(mapper);

            Services.AddMediatR(typeof(DiRegister).Assembly);
        }
    }
}
