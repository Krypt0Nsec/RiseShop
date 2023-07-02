using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rise.Services.Identity.DbContexts;
using Rise.Services.Identity.Models;
using Rise.Web.Services;
using Rise.Web.Services.IServices;

namespace Rise.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			builder.Services.AddHttpClient<IProductService, ProductService>();
			builder.Services.AddHttpClient<ICartService, CartService>();
			builder.Services.AddHttpClient<ICouponService, CouponService>();
			builder.Services.AddHttpClient<IOrderService, OrderService>();
			builder.Services.AddHttpClient<IAddressService, AddressService>();
			SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
			SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
			SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
			SD.OrderAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];
			SD.AddressAPIBase = builder.Configuration["ServiceUrls:AddressAPI"];

			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<ICartService, CartService>();
			builder.Services.AddScoped<ICouponService, CouponService>();
			builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IAddressService, AddressService>();
			var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("bağlantı adı 'conn' bulunamadı.");
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionstring));
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = "Cookies";
				options.DefaultChallengeScheme = "oidc";
			})
				.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
				.AddOpenIdConnect("oidc", options =>
				{
					options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];
					options.GetClaimsFromUserInfoEndpoint = true;
					options.ClientId = "Rise";
					options.ClientSecret = "secret";
					options.ResponseType = "code";
					options.ClaimActions.MapJsonKey("role", "role", "role");
					options.ClaimActions.MapJsonKey("sub", "sub", "sub");
					options.TokenValidationParameters.NameClaimType = "name";
					options.TokenValidationParameters.RoleClaimType = "role";
					options.Scope.Add("Rise");
					options.SaveTokens = true;

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
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();

		}
	}
}

