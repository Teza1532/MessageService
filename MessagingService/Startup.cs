﻿using MessageService.Data.Context;
using MessageService.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;

namespace MessagingService
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<MessageServiceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddAuthentication() //No default Parameter for Authentication as there is two different schemers to do the Authentication.
                .AddJwtBearer("Customer", options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = Configuration["Jwt:CustomerIssuer"],

                       ValidateAudience = true,
                       ValidAudience = Configuration["Jwt:CustomerAudience"],

                       ValidateLifetime = true,

                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes("this Key"))
                   };
                   options.SaveToken = true;
               })
               //OverLoad the AddJwt with seperate JwtBearer tokens
             .AddJwtBearer("Customer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:StaffIssuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:StaffAudience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("this Key"))
                };
                options.SaveToken = true;
            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(".."))
                .SetApplicationName("CustumerSecurity").SetApplicationName("StaffSecurity");

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MessageServiceCookie";
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder() //I change the Default policy so that it checks both schemes before exiting
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("Staff", "Customer")
                .Build();

                //We have two policies that we can now use on our Actions.
                options.AddPolicy("Staff", policy => policy.RequireClaim("StaffID"));
                options.AddPolicy("Customer", policy => policy.RequireClaim("UserID"));
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

                services.AddScoped<IMessageRepository, MessageRepository>();
                services.AddScoped<IStaffRepository, StaffRepository>();
                services.AddScoped<ICustomerRepository, CustomerRepository>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Route/action=Index}");
            });
        }
    }
}
