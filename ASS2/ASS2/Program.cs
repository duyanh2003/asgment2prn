using ASS2.Models;
using ASS2.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

namespace ASS2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Configure DbContext with a connection string
            var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Could not find connection string.");
            builder.Services.AddDbContext<MyStoreContext>(options =>
                options.UseSqlServer(connectionString));

            // Register your repositories with the scoped lifetime to make sure DbContext is used correctly
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<RepositoryManager>();

            // Add HttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set the session duration as needed
                    options.Cookie.IsEssential = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.SlidingExpiration = true;
                });


            var app = builder.Build();

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

            // Use authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/Login");
                    return Task.CompletedTask;
                });
            });




            app.Run();
        }
    }
}
