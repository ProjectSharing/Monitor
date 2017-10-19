using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Redis;
using JQCore.Configuration;
using JQCore.Dependency;
using JQCore.Mvc.Filter;
using JQCore.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Security.Claims;

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
                    //.Configure<RedisCacheOption>(Configuration)
                    ;
            services.AddHangfire(m =>
            {
                string connectionString = Configuration.GetValue<string>("Redis:Hangfire:TaskShedule");
                m.UseRedisStorage(connectionString, new RedisStorageOptions
                {
                    Prefix = "Monitor:TaskShedule:"
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy =>
                {
                    policy.Requirements.Add(new LoginRequirement());
                });
            })
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o => o.LoginPath = new PathString("/Account"))
                ;
            //×¢ÈëÊÚÈ¨Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacRegisterModule());
            ContainerManager.UseAutofacContainer(builder)
                            .UseRedis()
                            .UseRedisLock()
                            ;
            ApplicationContainer = (ContainerManager.Instance.Container as AutofacObjectContainer).Container;
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
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
            app.UseStaticFiles()
               .UseSession()
               .UseAuthentication();

            app.UseHangfireServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            applicationLifetime.RegisterRedisShutDown();
        }
    }
}