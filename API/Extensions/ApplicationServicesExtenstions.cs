
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtenstions
    {
        //I use this --> Because AddApplicationServices is an Extension Method
        //That means any IServiceCollection can use this method as an extension method
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<StoreContext>(options=> {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            //IConnectionMultiplexer --> I use it to connect to Redis DB
            services.AddSingleton<IConnectionMultiplexer>(c=>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Handle the Validation errors
            services.Configure<ApiBehaviorOptions>(options=>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
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
            services.AddCors(opt=>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("https://localhost:4200");
                });
            });
            return services;
        }
    }
}