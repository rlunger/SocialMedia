using AutoMapper;
using DotnetAssessmentSocialMedia.Data;
using DotnetAssessmentSocialMedia.Exception;
using DotnetAssessmentSocialMedia.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DotnetAssessmentSocialMedia
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // Automapper
            services.AddAutoMapper();
            
            // Configure swagger doc generation
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "SocialMedia", Version = "v1"}); 
                });
            
            // Configure EF Core DbContext
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<SocialMediaContext>(
                    options => options.UseNpgsql(Configuration.GetConnectionString("SocialMediaDatabase")))
                .BuildServiceProvider();

            // Add dependencies to IoC container
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IValidateService, ValidateService>();
            services.AddTransient<IHashtagService, HashtagService>();
            services.AddTransient<ITweetService, TweetService>();

            // Seeder used in Program.cs for development
            services.AddScoped<Seeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMedia v1");
                c.RoutePrefix = "api";
            });
            
            app.UseMvc();
        }
    }
}