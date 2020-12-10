using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        public Startup (IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(_Configuration.GetConnectionString("Default")));
            services.AddControllersWithViews();
            services.AddTransient<WebStoreDbInitializer>();
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, SqlProductData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseWelcomePage("/welcome") ;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); 
            });
        }
    }
}
