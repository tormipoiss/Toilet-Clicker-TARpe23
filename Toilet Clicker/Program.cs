using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.ApplicationServices.Services;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
using Toilet_Clicker.Security;

internal class Program
{
	private static void Main(string[] args)
	{
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IToiletsServices, ToiletsServices>();
        builder.Services.AddScoped<ILocationsServices, LocationsServices>();
        builder.Services.AddScoped<IFileServices, FileServices>();
        builder.Services.AddScoped<IEmailsServices, EmailsServices>();
        builder.Services.AddScoped<IAccountsServices, AccountsServices>();
        builder.Services.AddScoped<IPlayerProfilesServices, PlayerProfilesServices>();
        builder.Services.AddDbContext<ToiletClickerContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequiredLength = 3;

            options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
            .AddEntityFrameworkStores<ToiletClickerContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
            .AddDefaultUI();

        //all tokens
        builder.Services.Configure<DataProtectionTokenProviderOptions>(
            options => options.TokenLifespan = TimeSpan.FromHours(5)
            );

        //email tokens confirmation
        builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(
            options => options.TokenLifespan = TimeSpan.FromDays(3)
            );

        var app = builder.Build();
        app.UseStaticFiles();
        CreateDbIfNotExists(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ToiletClickerContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error ocurred while creating database");
            }
        }
    }
}