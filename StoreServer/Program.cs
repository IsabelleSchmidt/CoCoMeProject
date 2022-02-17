using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreServer.Data;
using StoreServer.Models.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<StoreServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreServerContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreServerContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
}

var supportedCultures = new string[] { "en-US" };
app.UseRequestLocalization(options =>

        options
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures)
        .SetDefaultCulture("en-US")
        .RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
        {
            return Task.FromResult(new ProviderCultureResult("en-US"));
        }))
);

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

app.Run();
