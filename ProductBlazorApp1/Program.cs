using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductBlazorApp1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Veritaban� ba�lant�s�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=productreviewNew.db"));

// Identity servisi
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

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

        var result = await userManager.CreateAsync(newUser, "User123!"); //�ifre
        if (result.Succeeded)
        {
            Console.WriteLine("Seed kullan�c� ba�ar�yla eklendi.");
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

app.UseAuthentication(); // Giri� i�lemleri i�in gerekli
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
