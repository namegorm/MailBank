using System.Linq;
using System.Net;
using Core.API.Authentication.Schemes.Handlers;
using Core.API.Authentication.Schemes.Options;
using Core.API.Filters;
using Core.API.Middlewares;
using Core.Application.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Products.API.Formatters.Input;
using Products.Application;
using Products.Application.ViewModels;
using Products.Application.ViewModelValidators;
using Products.Application.ViewModelValidators.Implementations;
using Products.Application.ViewModelValidators.Interfaces;
using Products.Infrastructure;

namespace Products.API
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddAuthentication("Client").AddScheme<ClientAuthenticationOptions, ClientAuthenticationHandler>("Client", null);

            services.AddControllers(options =>
            {
                options.Filters.Add<CoreAsyncExceptionFilter>();
                options.InputFormatters.Add(new ProductTextInputFormatter());
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.UseApiBehavior = false;
            });

            services.AddMvc().AddFluentValidation().ConfigureApiBehaviorOptions(options => 
            {
                options.InvalidModelStateResponseFactory = x =>
                {
                    var errors = string.Join('\n', x.ModelState.Values.Where(y => y.Errors.Count > 0)
                        .SelectMany(y => y.Errors)
                        .Select(y => y.ErrorMessage));

                    return new OkObjectResult(CoreResultModel.Create(HttpStatusCode.BadRequest, errors));
                };
            });

            services.AddTransient<IFirstProductNameValidator, FirstProductNameValidator>();
            services.AddTransient<ISecondProductNameValidator, SecondProductNameValidator>();
            services.AddTransient<IFirstProductDescriptionValidator, FirstProductDescriptionValidator>();
            services.AddTransient<ISecondProductDescriptionValidator, SecondProductDescriptionValidator>();

            services.AddTransient<IValidator<ProductViewModel>, ProductViewModelValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CoreResultMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
