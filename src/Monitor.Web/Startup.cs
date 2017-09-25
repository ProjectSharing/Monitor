using Autofac;
using Autofac.Extensions.DependencyInjection;
using JQCore.Configuration;
using JQCore.Dependency;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using System;

namespace Monitor.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(hostingEnvironment.ContentRootPath)
                          .AddJsonFile("appsettings.json", true, true)
                          ;
            Configuration = builder.Build();
            ConfigurationManage.SetConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            services.AddSession()
                    .AddMemoryCache()
                    .AddOptions()
                    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                    ;
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacRegisterModule());
            ApplicationContainer = builder.Build();
            ContainerManager.UseAutofacContainer(ApplicationContainer);
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}