using Heartbeats.Extensions;
using Heartbeats.Extentions;
using Heartbeats.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddChatbotServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityService();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await ApplicationDbInitializer.SeedRolesAndAdminAsync(services);
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();