using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductBlazorApp1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=productreviewNew.db"));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Kullanýcý ekleme iþlemi - async çalýþtýrýlmasý gereken blok
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var email = "user@user.com";
    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        var newUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, "User123!");
        if (result.Succeeded)
        {
            Console.WriteLine("Seed kullanýcý baþarýyla eklendi.");
        }
        else
        {
            foreach (var error in result.Errors)
                Console.WriteLine($"Hata: {error.Description}");
        }
    }
}

// admin
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminEmail = "admin@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, "Admin123!");
        if (result.Succeeded)
        {
            Console.WriteLine("Admin kullanýcý baþarýyla eklendi.");
        }
        else
        {
            foreach (var error in result.Errors)
                Console.WriteLine($"Hata: {error.Description}");
        }
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// login ile baþlat 
app.MapGet("/", ctx =>
{
    ctx.Response.Redirect("/login", permanent: false);
    return Task.CompletedTask;
});

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
