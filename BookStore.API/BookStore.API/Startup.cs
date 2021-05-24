using BookStore.API.Data;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API
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

            //**It is used for If we are not using Patch Methods for controllers action method
            //services.AddControllers();
            //**To use Patch Method We have to use 
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.API", Version = "v1" });
            });

            //**To tell about all Database(Entity frameWork) we have to add that service in this 
            //**Option is to add connection string we can add here or in the BookStoreContext class by override the OnConfiguring() method there 
            //services.AddDbContext<BookStoreContext>(); // if we added connection string in context class
            //**Get the connection string from appsettings.json i.e.BookStoreDB
            
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BookStoreDB")));
            //options => options.UseSqlServer("Server=FMIC00100\SQL2014FULL;Database=BooksStoreAPI;Integrated Security=True"));

            //**the following line will add Dependency Injection of Transient type for the project 
            services.AddTransient<IBookRepository, BookRepository>();

            //Add Automapper For mapping one class type to another class type
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
