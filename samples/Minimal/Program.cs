using AspNetCore.VersionInfo;
using AspNetCore.VersionInfo.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add AspNetCore.VersionInfo Providers
builder.Services.AddVersionInfo()
                .With<ClrVersionProvider>()
                .With<AssemblyVersionProvider>()
                .With<AppDomainAssembliesVersionProvider>()
                .With<EnvironmentProvider>()
                .With<EnvironmentVariablesProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
// Add AspNetCore.VersionInfo Endpoint
app.MapVersionInfo();

app.Run();
