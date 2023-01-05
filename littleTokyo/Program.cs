using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using littleTokyo.Data;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Text;
using Microsoft.VisualBasic;
using littleTokyo.Core;
using Constants = littleTokyo.Core.Constants;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddDbContext<littleTokyoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MenuContext") ?? throw new InvalidOperationException("Connection string 'MenuContext' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<littleTokyoContext>();
  
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseDeveloperExceptionPage();
    
}

using (var scope =  app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<littleTokyoContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
    });
}