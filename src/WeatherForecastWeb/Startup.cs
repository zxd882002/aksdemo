using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecastWeb.Repository;

namespace WeatherForecastWeb
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
            var configuration = Configuration.GetSection("Options");
            services.Configure<Options>(configuration);
            Options options = configuration.Get<Options>();
            List<ServiceConnection> serviceConnectionList = options.ServiceConnections;
            foreach (ServiceConnection serviceConnection in serviceConnectionList)
            {
                services.AddHttpClient(serviceConnection.ServiceName, c =>
                {
                    c.BaseAddress = new Uri(serviceConnection.BaseUrl);
                    c.Timeout = serviceConnection.Timeout;
                });
            }

            services.AddScoped(x => new WeatherForecastRepository(x.GetRequiredService<IHttpClientFactory>()));

            services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
