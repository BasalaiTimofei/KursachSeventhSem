using Backend.Context;
using Backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ShopDatabase")));

            services.AddMvc().AddJsonOptions(w =>
            {
                w.SerializerSettings.Formatting = Formatting.Indented;
            });

            services.AddScoped<AssessmentService>();
            services.AddScoped<BasketService>();
            services.AddScoped<CommentService>();
            services.AddScoped<OrderService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProviderService>();
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();

            services.AddScoped<Creating>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}