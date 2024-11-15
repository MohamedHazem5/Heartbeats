using Heartbeats.Extensions;
using Heartbeats.Extentions;
using Models;
using System.Diagnostics;

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();