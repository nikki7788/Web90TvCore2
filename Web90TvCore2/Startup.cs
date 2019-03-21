using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Repository;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // appsetting  تنطیم کردن خواندن از  
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //------------------------ set the database and its identity setting--------------------------------
            //verify Connection String To app
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("MyConnectionString")));

            //verify Idenity service
            services.AddIdentity<ApplicationUsers, ApplicationRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); //به هرکاربردر هنگام لاگین بودن یک توکن اختصاص میدهد  باتغیرر توکن لاگ وت میشود
            //-----------------------------------------------------------------------

            //-------------------------- set the services and Unit of work and repository setting ---------------------------------------------

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAspNetUserRolesRepo, AspNetUserRolesRepo>();

            //-----------------------------------------------------------------------

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();

                //------------برای آپدیت کردن کتابخانه های کلاینت مانند بوت استرپ جی کویری و ..------------------
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                    RequestPath = "/" + "node_modules"
                });
                //--------------------------------------------------=-------
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //------------خودم اضافه کردم todo: --------------------
            //app.UseStatusCodePages();
            //----------------------------------------------

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
