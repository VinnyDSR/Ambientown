using AmbienTown.Repositories.Context;
using AmbienTown.Utils.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AmbienTown.Utils.DI;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.Extensions.Logging;
using AmbienTown.Utils.Logs;

namespace AmbienTown
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
            //services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.WithOrigins("https://ambientown.ddns.net/")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTime;
                    //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    //options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
                    //options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            var settingsSection = Configuration.GetSection("AmbienTown");
            services.Configure<AppSettings>(settingsSection);

            var appSettings = settingsSection.Get<AppSettings>();

            services.AddDbContext<AmbienTownContext>((options) =>
            {
                options.UseSqlServer(appSettings.ConnectionString);
            });

            services.AddControllersWithViews();

            DependencyInjection.RegisterDependecies(services, appSettings);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AmbienTown API",
                    Version = "v1"
                });

                //C:\\ambientown deploy\\AmbienTown.xml
                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "C:\\Users\\Lineker Evangelista\\Documents\\IFSP\\TCC\\Repositorio2\\Desenvolvimento\\api\\AmbienTown\\AmbienTown.xml");
                //options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // Shows UseCors with named policy.
            app.UseCors("AllowAllHeaders");

            //app.UseCorsMiddleware();

            app.UseRouting();

            //app.UseCors(
            //    options => options.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials()
            //);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();                        

            //app.UseAuthorization();

            //app.UseDeveloperExceptionPage();

            loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information
            }));

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmbienTown API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<AmbienTownContext>())
            {
                context.Database.EnsureCreated();

                context.Seed();
            }
        }
    }
}