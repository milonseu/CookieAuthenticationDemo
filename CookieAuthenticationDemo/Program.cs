using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add MVC services
builder.Services.AddControllersWithViews();

// Cookie Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Login ?? ???? ????? redirect ???
        options.LoginPath = "/Account/Login";

        // Permission ?? ????? ????? redirect ???
        options.AccessDeniedPath = "/Account/AccessDenied";

        // Cookie-?? ???
        options.Cookie.Name = "MyCookieAuth";

        // Cookie ?????? ?????
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        // Browser ???? ????? cookie ?????
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ??
app.UseAuthentication();

// Authorization ??
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
