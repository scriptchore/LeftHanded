using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();

                        services.AddDbContext<StoreContext>(opt => {
                        opt.UseSqlite(config.GetConnectionString("lefthanded"));
                        });

                        services.AddScoped<IProductRepository, ProductRepository>();
                        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                        services.Configure<ApiBehaviorOptions>(options =>
                        {
                            options.InvalidModelStateResponseFactory = ActionContext =>
                            {
                                var errors = ActionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();

                                var errorResponse = new ApiValidationErrorResponse
                                {
                                    Errors = errors
                                };

                                return new BadRequestObjectResult(errorResponse);
                            };
                        });
            return services;
        }
    }
}