using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using Newtonsoft.Json.Serialization;
using WebAPI.Models;
using AutoMapper;
using WebAPI.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAPI.Models.Repositories;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build(); 
        }
        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddEntityFramework().AddSqlServer().AddDbContext<DBContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:ConnectionString"]);
            });     
            
            //to add the security
            services.AddIdentity<Communaute, IdentityRole>().AddEntityFrameworkStores<DBContext>(); 
            services.AddScoped<INGCookingRepository, NGCookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging")); 
            loggerFactory.AddDebug();
            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            Mapper.Initialize(config => {
                config.CreateMap<Recette, RecetteViewModel>(); 
                config.CreateMap<RecetteFromViewModel, Recette>();
                config.CreateMap<CommunauteFromViewModel, Communaute>();   
                config.CreateMap<Communaute, CommunauteViewModel>(); 
                config.CreateMap<Ingredient, IngredientViewModel>();  
                config.CreateMap<IngredientViewModel, Ingredient>();   
                config.CreateMap<Comment, CommentViewModel>();   
            });
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type, x-xsrf-token" }); 

                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    await next(); 
                } 
            });
            app.UseIdentity(); 
            app.UseMvc(); 
        }
        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
