using Northwind.BikeShop.FeatureManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Northwind.BikeShop.Authentication;

namespace Northwind.BikeShop
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
            //
            // Allow singletons to access HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //
            // Enable contrived query string authentication
            services.AddAuthentication(Schemes.QueryString)
                    .AddQueryString();

            //
            // Enable feature targeting
            services.AddSingleton<ITargetingContextAccessor, HttpContextTargetingContextAccessor>();

            //
            // Add feature management services
            // Add feature filters for conditionally enabling features
            services.AddFeatureManagement()
                    .AddFeatureFilter<BrowserFilter>()
                    .AddFeatureFilter<TargetingFilter>();

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //
            // Enable automatic refresh of Azure App Configuration
            app.UseAzureAppConfiguration();

            app.UseAuthentication();

            app.UseHttpsRedirection();
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
