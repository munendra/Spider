using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spider.Api.Filters;
using Spider.Api.Middleware;
using Spider.Application.Applications.Commands.CreateApplication;
using Spider.Application.Applications.Commands.Validator;
using Spider.Application.Infrastructure;
using Spider.Persistence;
using Spider.Service.Application;
using System.Reflection;
using FluentValidation;
using Spider.Application.Environments.Commands.CreateEnvironment;
using Spider.Application.Environments.Commands.Validator;
using Spider.Service.Configuration;
using Spider.Service.Environment;


namespace Spider.Api
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your createApplication, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddCors();
            services.AddSingleton(Configuration);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddMediatR(typeof(CreateApplicationCommand).GetTypeInfo().Assembly);
            services.AddDbContext<SpiderDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SpiderDatabase")));

            services.AddTransient<IEnvironmentService, EnvironmentService>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IValidator<CreateEnvironmentDto>, CreateConfigurationDtoValidator>();

            services
                .AddMvc(
                    options =>
                    {
                     
                        options.Filters.Add(new ApiResponseActionFilter());
                    })

                // .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                //.ConfigureApiBehaviorOptions(options =>
                //{
                //    options.SuppressConsumesConstraintForFormFileParameters = true;
                //    options.SuppressInferBindingSourcesForParameters = true;
                //    options.SuppressModelStateInvalidFilter = true;
                //    options.SuppressMapClientErrors = true;

                //    options.ClientErrorMapping[404].Link =
                //        "https://httpstatuses.com/404";
                //})
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<CreateApplicationDtoValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var allowOrigin = Configuration.GetSection("AllowOrigins:origins");
                app.UseCors(builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithOrigins(allowOrigin.Value);

                });
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionMiddleware();
            app.UseResponseCompression();
            app.UseMvcWithDefaultRoute();
        }
    }
}