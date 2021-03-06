using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vjezba.DAL;

namespace Vjezba.Web
{
	public class Startup
	{

		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllersWithViews().AddRazorRuntimeCompilation();

			services.AddDbContext<ClientManagerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ClientManagerDbContext"), opt => opt.MigrationsAssembly("Vjezba.DAL")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			var supportedCultures = new[] { new CultureInfo("hr"), new CultureInfo("en"), new CultureInfo("en-US"), new CultureInfo("en-GB") };

			app.UseRequestLocalization(new RequestLocalizationOptions {
				DefaultRequestCulture = new RequestCulture("hr"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute("kontakt-forma", "kontakt-forma", new { controller = "Home", action = "Contact" });

				endpoints.MapControllerRoute("o-aplikaciji", "o-aplikaciji/{lang:alpha:length(2)}", new { controller = "Home", action = "Privacy" });

				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
