using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //set up service container MVC - for dependency injection
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
                  
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //allows dev to see development errors - e.g when 500 error is given - provides stack
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    //forevery request printout "Hello world"
            //    await context.Response.WriteAsync("<html><body><h3>Hello World!</h3></body></html>");
            //});

            //require this to find and specify the default file - if this is what we want to use - in this case index.html
            //app.UseDefaultFiles();

            //allow use of static files from witin the root folder
            app.UseStaticFiles();

            //need middleware MVC to look for and assist with controllers
            //Handle the default mapping route - much like spring - for incomming requests

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

        }
    }
}
