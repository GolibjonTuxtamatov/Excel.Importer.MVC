//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Brokers.Spreadsheets;
using Excel.Importer.MVC.Brokers.Storages;
using Excel.Importer.MVC.Services.Foundations.Applicants;
using Excel.Importer.MVC.Services.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Spreadsheets;
using Excel.Importer.MVC.Services.Orchestrations.Applicants;
using Excel.Importer.MVC.Services.Orchestrations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Spreadsheets;
using Excel.Importer.MVC.Services.Proccessings.Applicants;
using Excel.Importer.MVC.Services.Proccessings.Groups;
using Excel.Importer.MVC.Services.Proccessings.Spreadsheets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Excel.Importer.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<StorageBroker>();

            AddBrokers(services);
            AddServices(services);
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

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ISpreadsheetBroker, SpreadsheetBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ISpreadsheetOrchestrationService, SpreadsheetOrchestrationService>();
            services.AddTransient<IApplicantService, ApplicantService>();
            services.AddTransient<IApplicantProccessingService, ApplicantProccessingService>();
            services.AddTransient<IApplicantOrchestrationService, ApplicantOrchestrationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupProccessingService, GroupProccessingService>();
            services.AddTransient<IGroupOrchestrationService, GroupOrchestrationService>();
            services.AddTransient<ISpreadsheetService, SpreadsheetService>();
            services.AddTransient<ISpreadsheetProccessingService, SpreadsheetProccessingService>();
        }
    }
}
