using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopWebsite.Infrastructure;

namespace ShopWebsite
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
            services.AddSession();
            services.AddControllersWithViews();

            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ShopDbContext")));
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                    "pages",
                                    "{slug?}",
                                    defaults: new { controller = "Pages", action = "Page" }
                            );

                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                   );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //,
                //    defaults: new { action = "Index" }
                //       );

                //        endpoints.MapControllerRoute(
                //    name: "carPaginationWithIndex",
                //pattern: "{area:exists}/{controller=Home}/{action=Index}/{page?}"
                //       );

                //        endpoints.MapControllerRoute(
                //   name: "areas",
                //   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id}"
                //      );


            });
        }
    }
}
