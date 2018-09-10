using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication1
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
            services
                .AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonFormatters()
                .AddApiExplorer()
                .AddVersionedApiExplorer(setup => // Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer package
                {
                    setup.GroupNameFormat = "'v'VV";
                });

            services.AddApiVersioning(setup => // Microsoft.AspNetCore.Mvc.Versioning package
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.ApiVersionReader = new HeaderApiVersionReader("api-version");
                setup.ReportApiVersions = true;
            });

            services.AddSwaggerGen(setup =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider()
                    .GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (Microsoft.AspNetCore.Mvc.ApiExplorer.ApiVersionDescription description
                    in provider.ApiVersionDescriptions)
                {
                    setup.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }

                // add a custom operation filter which sets default values
                setup.OperationFilter<SwaggerDefaultValues>();
                setup.OperationFilter<SwaggerHeaders>();
                setup.OperationFilter<ExamplesOperationFilter>();

                // integrate xml comments
                setup.IncludeXmlComments(XmlCommentsFilePath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                setup =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (Microsoft.AspNetCore.Mvc.ApiExplorer.ApiVersionDescription description
                        in provider.ApiVersionDescriptions)
                    {
                        setup.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var fileName = System.IO.Path.GetFileName(assemblyName + ".xml");
                //Set the comments path for the swagger json and ui.
                var filePath = System.IO.Path.Combine(basePath, fileName);
                return filePath;
            }
        }

        private static Swashbuckle.AspNetCore.Swagger.Info CreateInfoForApiVersion(
            Microsoft.AspNetCore.Mvc.ApiExplorer.ApiVersionDescription description)
        {
            var info = new Swashbuckle.AspNetCore.Swagger.Info()
            {
                Title = $"Sample API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
                Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                {
                    Name = "Bill Mei",
                    Email = "bill.mei@somewhere.com"
                },
                TermsOfService = "Shareware",
                License = new Swashbuckle.AspNetCore.Swagger.License()
                {
                    Name = "MIT",
                    Url = "https://opensource.org/licenses/MIT"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }

    /// <summary>
    ///	Example filter to add required CompanyId header
    ///	https://alexdunn.org/2018/06/29/adding-a-required-http-header-to-your-swagger-ui-with-swashbuckle/
    /// </summary>
    public class SwaggerHeaders : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation,
            Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<Swashbuckle.AspNetCore.Swagger.IParameter>();

            operation.Parameters.Add(new Swashbuckle.AspNetCore.Swagger.NonBodyParameter
            {
                Name = "CompanyId",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }

    /// <summary>
    ///	https://github.com/Microsoft/aspnet-api-versioning/wiki/Swashbuckle-Integration
    /// </summary>
    public class SwaggerDefaultValues : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation,
            Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<Swashbuckle.AspNetCore.Swagger.NonBodyParameter>())
            {
                Microsoft.AspNetCore.Mvc.ApiExplorer.ApiParameterDescription description =
                    context
                        .ApiDescription
                        .ParameterDescriptions
                        .FirstOrDefault(p => p.Name == parameter.Name);

                if (description == null)
                {
                    continue;
                }

                Microsoft.AspNetCore.Mvc.ApiExplorer.ApiParameterRouteInfo routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
