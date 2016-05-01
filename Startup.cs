using Microsoft.Owin;
using SJKP.InAzure.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace SJKP.InAzure.Web
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using Swashbuckle.Application;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Swagger UI
            httpConfiguration
            .EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
            .EnableSwaggerUi();

            app.UseWebApi(httpConfiguration);


            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var options = new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./"),
                EnableDirectoryBrowsing = false,
            };
            app.Use(typeof(DefaultFileRewriterMiddleware), options);

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(options);

           

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
