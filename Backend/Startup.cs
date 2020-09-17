using Backend.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Repository;
using Repository.Migrations;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartup(typeof(Backend.Startup))]
namespace Backend
{
    public class Startup
    {
        internal static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();
        internal static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); //apenas para agilizar

            var config = new HttpConfiguration();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureOAuth(app);

            app.UseWebApi(config);
            ConfigureJSON(config);

            using (var context = new LivrariaContext())
            {
                var migrationConfiguration = new Configuration();
                migrationConfiguration.RunSeed(context);
            }
        }

        private static void ConfigureJSON(HttpConfiguration config)
        {
            JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            JsonSerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            JsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            JsonSerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Include;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings = JsonSerializerSettings;
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
                Provider = new CustomAuthorizationServerProvider(),
                //RefreshTokenProvider = new CustomRefreshTokenProvider()
            };

            //use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}