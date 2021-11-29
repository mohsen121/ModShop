using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application;
using ModShop.Domain.Entities;
using ModShop.Infrustracture;
using ModShop.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using ModShop.Application.Common.Interfaces;
using ModShop.RazorPages.Services;
using ModShop.RazorPages.Common;

namespace ModShop.RazorPages
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
            services.AddMemoryCache();
            //services.AddRazorPages(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Pages/_Administrator");
            //    options.Conventions.AuthorizeFolder("/Pages/_User");
            //});
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDb>()
                .AddDefaultTokenProviders();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //    .AddInMemoryApiResources(Config.GetApis())
            //    //.AddInMemoryApiScopes(Config.GetClients())
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddAspNetIdentity<ApplicationUser>();

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddInfrustracture(Configuration);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // برای اینکه کاربران بتوانند بدون ایمیل ثبت نام کنند غیرفعال شد 
                options.User.RequireUniqueEmail = false;

                // باید شماره موبایل کاربر کانفیرم شده باشد قبل از لاگین
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    //options.Cookie.Expiration = TimeSpan.FromDays(150);
            //    options.ExpireTimeSpan = TimeSpan.FromDays(150);
            //    options.LoginPath = "/Administrator98/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
            //    options.LogoutPath = "/Administrator98/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
            //    options.AccessDeniedPath = "/Administrator98/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
            //    options.SlidingExpiration = true;

            //});


            services.AddRazorPages(options =>
            {
                //options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                //options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                options.Conventions.AuthorizeFolder("/_Adminstrator");
                options.Conventions.AuthorizeFolder("/_User");
                //options.Conventions.AddPageRoute("/Home/Index", "");
                //options.Conventions.AddPageRoute("/Home/Index", "/Index");
            }).AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = ctx =>
                    {
                        var requestPath = ctx.Request.Path;
                        if (requestPath.Value.Contains("/Administrator"))
                        {
                            //باید روت های متناسب در اینجا درج شود و نه محل پیج آن
                            ctx.Response.Redirect("/Administrator/Account/AccessDenied");
                        }
                        else /*if (requestPath.Value.Contains("/_Profile"))*/
                        {
                            ctx.Response.Redirect("/Account/AccessDenied");
                        }

                        return Task.CompletedTask;
                    },

                    OnRedirectToReturnUrl = ctx =>
                    {
                        return Task.CompletedTask;
                    },

                    OnRedirectToLogin = ctx =>
                    {
                        var requestPath = ctx.Request.Path;
                        if (requestPath.Value.Contains("/Adminstrator"))
                        {
                            if (!ctx.HttpContext.Request.Path.Value?.EndsWith("/Adminstrator/Account/Login", StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ctx.Response.Redirect("/Adminstrator/Account/Login");
                        }
                        else/* if (requestPath.Value.Contains("/_Profile"))*/
                        {
                            if (!ctx.HttpContext.Request.Path.Value?.EndsWith("/Adminstrator/Account/Login", StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ctx.Response.Redirect("/Account/Login");
                        }

                        return Task.CompletedTask;
                    },

                    OnRedirectToLogout = ctx =>
                    {
                        var requestPath = ctx.Request.Path;
                        if (requestPath.Value.Contains("/Adminstrator"))
                        {
                            if (!ctx.HttpContext.Request.Path.Value?.EndsWith("/Adminstrator/Account/Logout", StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ctx.Response.Redirect("/Adminstrator/Account/Logout");

                        }
                        else/* if (requestPath.Value.Contains("/_Profile"))*/
                        {
                            if (!ctx.HttpContext.Request.Path.Value?.EndsWith("/Account/Logout", StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ctx.Response.Redirect("/Account/Logout");
                        }

                        return Task.CompletedTask;
                    },

                    OnSignedIn = ctx =>
                    {
                        return Task.CompletedTask;
                    },

                    OnSigningIn = ctx =>
                    {
                        return Task.CompletedTask;
                    },

                    OnSigningOut = ctx =>
                    {
                        return Task.CompletedTask;
                    },

                    OnValidatePrincipal = ctx =>
                    {
                        return Task.CompletedTask;
                    }
                };

                //options.LoginPath = $"/Identity/Account/Login";
                //options.LogoutPath = $"/Identity/Account/Logout";
                //options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            //CSRF
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });

            // DataTables.AspNet registration with default options.
            //services.RegisterDataTables();

            //Permissions
            //services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddPermissionService();


            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDb db,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseDeveloperExceptionPage();

            //app.UseCustomExceptionHandler();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //db.Database.Migrate();

            if (!db.Users.Any())
            {
                var user = new User
                {
                    Email = "info@ModShop.ir",
                    Created = DateTime.Now,
                    UserName = "ModShop",
                    PhoneNumber = "09365772241"
                };
                userManager.CreateAsync(user, "12345678").Wait();
                roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });



        }
    }
}
