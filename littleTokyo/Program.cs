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

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddRoles<IdentityRole>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<littleTokyoContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    }
    )
    .AddEntityFrameworkStores<littleTokyoContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();

}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<littleTokyoContext>();

    var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
    IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
}



app.Run();



