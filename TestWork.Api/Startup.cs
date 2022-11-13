using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TestWork.Db;
using TestWork.Services.Services.EmailSender;

namespace TestWork.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[]
{
                Assembly.GetExecutingAssembly(),
                Assembly.GetEntryAssembly(),
                Assembly.GetAssembly(typeof(TestWorkDbContext))
            };

            services.AddMediatR(assemblies);
            services.AddAutoMapper(assemblies);
            services.AddMvc();
            services.AddControllers();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(assemblies));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestWork.Api", Version = "v1" });

                c.CustomSchemaIds(x => x.FullName.Replace("+", "."));
            });

            services.AddDbContext<TestWorkDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });

            services.AddTransient<IEmailSenderService, EmailSenderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TestWorkDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWork.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbContext.Database.Migrate();
        }
    }
}