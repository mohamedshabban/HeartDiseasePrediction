using HeartDiseasePrediction.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System;

namespace HeartDiseasePrediction
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
			services.AddControllersWithViews();
			services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
			{
				ProgressBar = true,
				PositionClass = ToastPositions.TopRight,
				PreventDuplicates = true,
				CloseButton = true
			});
			services.AddMvc().AddRazorRuntimeCompilation();
			services.AddDistributedMemoryCache();
			//services.AddIdentity<ApplicationUser, IdentityRole>()
			//	.AddEntityFrameworkStores<AppDbContext>()
			//	.AddDefaultTokenProviders();
			services.AddHttpClient<DoctorController>();
			services.AddHttpClient<PatientController>();
			services.AddHttpClient<MedicalAnalystController>();
			services.AddHttpClient<ReciptionistController>();
			services.AddHttpClient<PrescriptionController>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSession(options =>
			{
				options.Cookie.Name = ".NetEscapades.Session";
				options.IdleTimeout = TimeSpan.FromDays(1);
				options.Cookie.IsEssential = true;
			});

			services.AddSession();
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
