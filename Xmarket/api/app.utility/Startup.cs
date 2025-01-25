using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Web.Xmarket.Startup))]  // Define el punto de entrada OWIN

namespace Web.Xmarket
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Middleware que registra la ejecución
            app.Use(async (context, next) =>
            {
                System.Diagnostics.Debug.WriteLine("Solicitud recibida en OWIN Middleware");
                await next.Invoke(); // Llama al siguiente middleware
            });

            // Middleware de autenticación con cookies
       

            // Middleware final
            app.Run(async context =>
            {
                await context.Response.WriteAsync("¡Hola desde OWIN en .NET 4.8!");
            });
        }
    }
}
