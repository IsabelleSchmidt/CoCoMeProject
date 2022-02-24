using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreServer.Data;
using StoreServer.Models;
using StoreServer.Models.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/api/data", async (StoreServerContext context) =>
    {
        List<Order> order = await context.Order.Include(order => order.OrderItems).ToListAsync();
        order.ForEach(orderItem => orderItem.OrderItems.ToList().ForEach(orderItem => orderItem.Order = null));
        return order;
    });
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/api/fetchitems", async (StoreServerContext context) =>
    {
        List<InventoryItem> inventoryItems = await context.InventoryItem.Include(item => item.ItemIdentifier).ToListAsync();
        List<ReturnedItem> returnedItem = new List<ReturnedItem>();
        inventoryItems.ForEach(inventoryItem => returnedItem.Add(new ReturnedItem(inventoryItem.ID, inventoryItem.ItemIdentifier.Name, inventoryItem.Count)));
        return returnedItem;
    });
});

app.MapGet("/api/removeitems/{id}/{removeCount}", async (int id, int removeCount, StoreServerContext context) =>
{
    if (await context.InventoryItem.FindAsync(id) is InventoryItem inventoryItem)
    {
        if (inventoryItem.Count - removeCount < 0) 
        {
            inventoryItem.Count = 0;
            context.InventoryItem.Update(inventoryItem);
        } else 
        {
            inventoryItem.Count -= removeCount;
            context.InventoryItem.Update(inventoryItem);
        }
        
        await context.SaveChangesAsync();
        return Results.Ok(inventoryItem);
    }

    return Results.NotFound();
});

app.Run();
