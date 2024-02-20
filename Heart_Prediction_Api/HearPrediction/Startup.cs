using HearPrediction.Api.Data.Services;
using HearPrediction.Api.Helpers;
using HearPrediction.Api.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace HearPrediction.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<JWT>(Configuration.GetSection("JWT"));

			//services.AddIdentity<Database.Entities.ApplicationUser, IdentityRole>()
			//	.AddEntityFrameworkStores<Database.Entities.AppDbContext>()
			//	.AddDefaultTokenProviders();
			services.Configure<DataProtectionTokenProviderOptions>(options =>
			options.TokenLifespan = TimeSpan.FromHours(10));

			services.AddIdentity<ApplicationUser, IdentityRole>(/*options =>options.SignIn.RequireConfirmedAccount = true*/)
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<JWT>();

			services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(Configuration
			.GetConnectionString("DefaultConnectionString")
			//,x => x.MigrationsAssembly("Database")
			));

			//Configuration of sending Email
			//var emailSetting = Configuration.GetSection("MailSettings").Get<MailSettings>();
			//services.AddSingleton(emailSetting);
			services.AddScoped<IMailServices, MailServices>();
			//services.Configure<IdentityOptions>(options =>
			//    options.SignIn.RequireConfirmedEmail = true
			//);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o =>
				{
					o.RequireHttpsMetadata = false;
					o.SaveToken = true;
					o.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidIssuer = Configuration["JWT:Issuer"],
						ValidAudience = Configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
						ClockSkew = TimeSpan.Zero
					};
				});

			services.AddControllers();
			services.AddControllersWithViews()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
				options.JsonSerializerOptions.PropertyNamingPolicy = null; // preven
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "HeartPrediction", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please Enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id="Bearer",
							},

							Name="Bearer",
							In=ParameterLocation.Header
						},
						new string[]{}
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HearPrediction.Api.v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
