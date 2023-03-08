using AspNetCore.VersionInfo.Providers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.VersionInfo.Samples.Authentication
{
    public class Startup
    {
        private const string VERSIONINFO_POLICY = nameof(VERSIONINFO_POLICY);
        private const string COOKIE_SCHEME = "SampleSchemeName";

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
                cfg.AddPolicy(name: VERSIONINFO_POLICY, cfgPolicy =>
                {
                    cfgPolicy.AddRequirements().RequireAuthenticatedUser();
                    cfgPolicy.AddAuthenticationSchemes(COOKIE_SCHEME);
                });
            });

            services.AddAuthentication(COOKIE_SCHEME) // Sets the default scheme to cookies
                    .AddCookie(COOKIE_SCHEME, options =>
                    {
                        options.AccessDeniedPath = "/AccessDenied";
                        options.LoginPath = "/Login";
                    });

            services.AddVersionInfo()
                .With<ClrVersionProvider>()
                .With<AssemblyVersionProvider>()
                .With<AppDomainAssembliesVersionProvider>()
                .With<EnvironmentProvider>()
                .With<EnvironmentVariablesProvider>();
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
                endpoints.MapVersionInfo().RequireAuthorization(VERSIONINFO_POLICY); ;
            });
        }
    }
}
