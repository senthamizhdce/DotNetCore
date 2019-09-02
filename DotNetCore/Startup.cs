using DotNetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace DotNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvcCore();//Basic future
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.Formatting = Formatting.Indented;                        
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            UseException(app, env);
            UseAuthentication(app);
            UseMVC(app);
            //Other Middlewares.
            app.UseHsts(); //HTTP Strict Transport Security


            UseAnonimouseMethod(app);
        }

        private static void UseAuthentication(IApplicationBuilder app)
        {
            //app.UseAuthentication();
            //app.UseMiddleware<AuthenticationMiddleware>();
            app.UseWhen(x => (x.Request.Path.StartsWithSegments("/api/Authentication", StringComparison.OrdinalIgnoreCase)),
            builder =>
            {
                builder.UseMiddleware<AuthenticationMiddleware>();
            });
            //app.Map("/api/Authentication", AuthenticationMiddleware); todo

        }

        private static void UseAnonimouseMethod(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Before Use;\n\n");
                await next.Invoke();
                await context.Response.WriteAsync("After Use;\n\n");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Invalid Route;\n\n");
            });
        }

        private static void UseMVC(IApplicationBuilder app)
        {
            //app.UseMvcWithDefaultRoute();
            //Routing - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2
            //When you run project it will start on "launchUrl". "launchUrl" is set to "api/values" in the project template. Nothing to do with MVC route that you changed
            //app.UseMvc(routes =>
            //{
            //    //routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //    routes.MapRoute(
            //    name: "default",
            //    template: "api/{controller}/{action}",
            //    defaults: new { controller = "Exception", action = "ThrowException" });
            //});
            app.UseMvc();
        }

        private static void UseException(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDatabaseErrorPage();
                //app.UseDeveloperExceptionPage(); //HTML Error Response. With Stack: Error Line, Query, Cookies and Header.
            }
            else
            {
                //app.UseExceptionHandler();
            }

            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            //    var exception = feature.Error;

            //    var result1 = JsonConvert.SerializeObject(new { error = exception.Message, sss = "" });
            //    var result = JsonConvert.SerializeObject(new VMResponse()
            //    {
            //        Error = new List<VMError>() {new VMError()
            //            {
            //                DisplayMessage = "Request Faild",
            //                ErrorMessage = exception.Message,
            //                RequestURI = context.Request.Path
            //            }
            //        },
            //        Status = false
            //    });
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(result);
            //}));
        }
    }
}
