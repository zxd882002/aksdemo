using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecastAPI.ConfigOptions;
using WeatherForecastAPI.Infrastructure.Encryption;
using WeatherForecastAPI.Infrastructure.Redis;
using WeatherForecastAPI.Models.NumberGuess;

namespace WeatherForecastAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // options            
            services.Configure<WeatherForecastApiOptions>(Configuration.GetSection(WeatherForecastApiOptions.SectionName));

            // redis            
            services.AddSingleton<IRedisHelper, RedisHelper>();

            // dev env: cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                    .WithOrigins("http://localhost:8080", "http://10.80.151.95:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            // controller
            services.AddControllers();

            // models
            services.AddSingleton<GameStatus>();
            services.AddSingleton<ECDsSigner>();

            // swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
