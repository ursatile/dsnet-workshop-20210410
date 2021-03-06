using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autobarn.Data;
using Autobarn.Website.GraphQL.Schemas;
using EasyNetQ;
using GraphiQl;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autobarn.Website.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Autobarn.Website {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddControllersWithViews();

			var bus = RabbitHutch.CreateBus(AMQP);
			services.AddSingleton<IBus>(bus);

			var db = new InMemoryCarDatabase("JsonData");

			services.AddSingleton<ICarDatabase>(db);
			services.AddSingleton<AutobarnSchema>();
			services
				.AddGraphQL(options => options.EnableMetrics = false)
				.AddSystemTextJson();

			services.AddSignalR();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseGraphQL<AutobarnSchema>();
			app.UseGraphiQl("/graphiql");

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapHub<NewCarHub>("/newcarhub");
			});
		}
	}
}