
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rise.Service.OrderAPI.DbContexts;
using Rise.Service.OrderAPI.Messaging;
using Rise.Service.OrderAPI.RabbitMQSender;
using Rise.Service.OrderAPI.Repository;
using Rise.Services.OrderAPI.Repository;
using Rise.Services.OrderAPI.Repository.IRepository;

namespace Rise.Services.OrderAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
						   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


			IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
			builder.Services.AddSingleton(mapper);
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddScoped<IOrderAdminRepository, OrderAdminRepository>();


			var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

			builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
			builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
			builder.Services.AddSingleton(new OrderRepository(optionBuilder.Options));

			builder.Services.AddSingleton<IRabbitMQOrderMessageSender, RabbitMQOrderMessageSender>();
			builder.Services.AddControllers();

			builder.Services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{

					options.Authority = "https://localhost:44365/";
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = false
					};

				});

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("ApiScope", policy =>
				{
					policy.RequireAuthenticatedUser();
					policy.RequireClaim("scope", "rise");
				});
			});

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Infotech.Services.OrderAPI", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"Enter 'Bearer' [space] and your token",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							},
							Scheme="oauth2",
							Name="Bearer",
							In=ParameterLocation.Header
						},
						new List<string>()
					}

				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Infotech.Services.OrderAPI v1"));
			}



			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.MapControllers();

			app.Run();
		}
	}
}