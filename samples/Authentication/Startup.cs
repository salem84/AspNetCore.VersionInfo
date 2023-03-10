using AspNetCore.VersionInfo.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.VersionInfo.Samples.Authentication
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
            services.AddRazorPages();

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(name: Constants.VERSIONINFO_USER_POLICY, cfgPolicy =>
                {
                    cfgPolicy.AddRequirements().RequireAuthenticatedUser();
                    cfgPolicy.AddAuthenticationSchemes(Constants.COOKIE_SCHEME);
                });

                cfg.AddPolicy(name: Constants.VERSIONINFO_ADMIN_POLICY, cfgPolicy =>
                {
                    cfgPolicy.AddRequirements().RequireAuthenticatedUser().RequireRole(Constants.ADMIN_ROLE);
                    cfgPolicy.AddAuthenticationSchemes(Constants.COOKIE_SCHEME);
                });
            });

            services.AddAuthentication(Constants.COOKIE_SCHEME)
                    .AddCookie(Constants.COOKIE_SCHEME, options =>
                    {
                        options.AccessDeniedPath = "/AccessDenied";
                        options.LoginPath = "/Login";
                    });

            services.AddHttpContextAccessor();

            services.AddVersionInfo()
                .With<ClrVersionProvider>()
                .With<AssemblyVersionProvider>()
                .With<AppDomainAssembliesVersionProvider>()
                .With<EnvironmentProvider>()
                .With<AuthEnvironmentVariablesProvider>(); // Require AdminRole (see implementation) 
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapVersionInfo().RequireAuthorization(Constants.VERSIONINFO_USER_POLICY); ;
            });
        }
    }
}
