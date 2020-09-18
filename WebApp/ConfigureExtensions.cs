using System;
using System.Linq;
using Application.Middlewares;
using Application.Services;
using Core.Interfaces;
using Data;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Process.Repository;
using Shared;
using WebApp;

namespace Web
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, Role>(
                options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            return services;
        }


        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {

                var context = services.BuildServiceProvider().GetService<ApplicationDbContext>();
                var categories = context.PermissionCategoryPermissions
                    .Include(c => c.Permission)
                    .Include(c => c.Category);


                foreach (var permissionCategory in categories)
                {
                    //Usage : user_add
                    options.AddPolicy(permissionCategory.Category.Label.ToLower() + "_" + permissionCategory.Permission.Label.ToLower(),
                        policy => policy.Requirements.Add(new PermissionRequirement(
                            new PermissionRequirementModel(permissionCategory.PermissionId, permissionCategory.CategoryId)
                            )));

                }


            });
            return services;
        }

        public static IServiceCollection ConfigureHangfire(this IServiceCollection services)
        {
            //services.AddHangfire(configuration => configuration
            //    .UsePostgreSqlStorage(Startup.StaticConfig.GetConnectionString("DefaultPostgre"),
            //        new PostgreSqlStorageOptions
            //        {
            //            SchemaName = "hangfire"
            //        }));

            services.AddHangfire(configuration => configuration
                .UseSqlServerStorage(Startup.StaticConfig.GetConnectionString("DefaultSQL"),
                    new SqlServerStorageOptions()
                    {
                        SchemaName = "hangfire"
                    }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            return services;
        }
        public static IServiceCollection ConfigureAppCookie(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                options.LogoutPath = "/Logout";
                options.LoginPath = "/Login"; // Set here your login path.
                options.AccessDeniedPath = "/Error/?errCode=404"; // set here your access denied path.

                options.SlidingExpiration = true;

            });
            return services;
        }
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Startup.StaticConfig.GetConnectionString("DefaultSQL")));

            //services.AddDbContext<ApplicationDbContext>(
            //options => options.UseNpgsql(Startup.StaticConfig.GetConnectionString("DefaultPostgre")));
            return services;
        }
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
            services.AddScoped<IRepository<UserPermission>, Repository<UserPermission>>();

            //services.AddScoped<IVehicleRepository, VehicleRepository>();
            
            services.AddScoped<IRepository<PermissionCategory>, Repository<PermissionCategory>>();

            //services.AddScoped<IRepository<ReportSettingFile>, Repository<ReportSettingFile>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IRepository<UserType>, Repository<UserType>>();
            services.AddScoped<IRepository<Likes>, Repository<Likes>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<UserDetail>, Repository<UserDetail>>();
            services.AddScoped<IRepository<TagPost>, Repository<TagPost>>();
  

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services.AddSingleton<EmailService>();
            services.AddScoped<UserService>();
            services.AddScoped<RoleService>();
            services.AddScoped<ExportService>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            return services;

        }

    }
}
