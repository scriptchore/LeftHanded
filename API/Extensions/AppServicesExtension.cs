using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity;
using INFRASTRUCTURE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


                   

                        services.AddDbContext<AppIdentityDbContext>(opt => {
                        opt.UseSqlServer(config.GetConnectionString("IdentityConnection"));
                        });

                        services.AddSingleton<IConnectionMultiplexer>(c =>
                        {
                            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));

                            var res = ConnectionMultiplexer.Connect(options);

                            return res;
                        });

                        services.AddSingleton<IResponseCacheService, ResponseCacheService>();


                        services.AddScoped<IBasketRepository, BasketRepository>();
                        services.AddScoped<IOrderService, OrderService>();
                        services.AddScoped<IUnitOfWork, UnitOfWork>();


                        services.AddScoped<IProductRepository, ProductRepository>();
                        services.AddScoped<IPaymentService, PaymentService>();
                        services.AddScoped<ITokenService, TokenService>();
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

                        services.AddCors(opt => 
                        {
                            opt.AddPolicy("CorsPolicy", policy => 
                            {
                                policy.AllowAnyHeader()
                                .AllowAnyMethod().WithOrigins("https://localhost:4200");
                            
                            });
                        });

            return services;
        }
    }
}