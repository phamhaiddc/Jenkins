using Application.Handlers;
using Domain.Interfaces;
using Infracstructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Cấu hình Entity Framework Core
            services.AddScoped<IRepository, Repository>();
            services.AddMediatR(typeof(CreateToDoHandler).Assembly);
            services.AddOcelot();
            // Cấu hình CORS nếu cần
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Sử dụng CORS nếu đã cấu hình
            app.UseCors("CorsPolicy");
            app.UseOcelot().Wait();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
