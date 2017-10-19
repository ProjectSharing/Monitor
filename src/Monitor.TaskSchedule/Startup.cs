using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Redis;
using JQCore.Configuration;
using JQCore.Dependency;
using JQCore.Hangfire;
using JQCore.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monitor.Application;
using NLog.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Monitor.TaskSchedule
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
            services.AddHangfire(m =>
            {
                string connectionString = Configuration.GetValue<string>("Redis:Hangfire:TaskShedule");
                m.UseRedisStorage(connectionString, new RedisStorageOptions
                {
                    Prefix = "Monitor:TaskShedule:"
                });
            });

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
            }

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/TaskScheduling", options: new DashboardOptions
            {
                AppPath = Configuration.GetValue<string>("WebSite"),
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.Map("/SynchroTask", r =>
            {
                r.Run(context =>
                {
                    using (var scope = ContainerManager.BeginLeftScope())
                    {
                        var sysConfigApplication = scope.Resolve<ISysConfigApplication>();
                        var sysConfigSynchroTaskID = Guid.NewGuid().ToString("N");
                        TaskScheldulingUtil.CreateRecurringJob(sysConfigSynchroTaskID, () => sysConfigApplication.SynchroConfigAsync(), cronExpression: "0/10 * * * *");
                        var servicerApplication = scope.Resolve<IServicerApplication>();
                        var servicerSynchroTaskID = Guid.NewGuid().ToString("N");
                        TaskScheldulingUtil.CreateRecurringJob(servicerSynchroTaskID, () => servicerApplication.SynchroServiceAsync(), cronExpression: "0/10 * * * *");
                        var projectApplication = scope.Resolve<IProjectApplication>();
                        var projectSynchroTaskID = Guid.NewGuid().ToString("N");
                        TaskScheldulingUtil.CreateRecurringJob(projectSynchroTaskID, () => projectApplication.SynchroProjectAsync(), cronExpression: "0/10 * * * *");
                        applicationLifetime.ApplicationStopping.Register(() =>
                        {
                            TaskScheldulingUtil.RemoveRecurringJobIfExists(sysConfigSynchroTaskID);
                            TaskScheldulingUtil.RemoveRecurringJobIfExists(servicerSynchroTaskID);
                            TaskScheldulingUtil.RemoveRecurringJobIfExists(projectSynchroTaskID);
                        });
                    }
                    return context.Response.WriteAsync("Create SynchroConfig Success");
                });
            });

            app.Run(context =>
            {
                return context.Response.WriteAsync("WelCome To TaskScheduling");
            });

            applicationLifetime.RegisterRedisShutDown();
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();

            //// Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid);
            return true;
        }
    }
}