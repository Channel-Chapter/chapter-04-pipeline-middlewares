using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiddlewareHandsOn.Api.ExceptionProviders;

namespace MiddlewareHandsOn.Api
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Abstraindo do arquivo Startup.cs, colocada na pasta Exceptions.
            app.UseCustomExceptionHandlerMiddleware(env);

            app.Use(async (context, next) =>
            {
                /*
                 * Criamos uma exception customizada e a lançamos aqui,
                 * causando propositalmente o curto circuito na pipeline.
                 * Para que a pipeline não continue e volte ao ExceptionHandlerMiddleware.
                 *
                 * Para testar a exception na pipeline descomente as linhas abaixo (46, 47 e 48).
                 */

                //throw new ApiCustomException(
                //    message: "Exception thrown during the 1st middleware execution.",
                //    errorCode: ApiCustomException.XPTO_ERROR);


                /*
                 * _logger.LogWarning será exibido no console de debug (output)
                 * quando a aplicação for chamada no navegador.
                 */

                _logger.LogWarning("\n1st delegate begins");

                // Chama o middleware 2.
                await next();

                // Executa após finalizar todos os outros middlewares (Middleware 2, 3, n).
                _logger.LogWarning("\n1st delegate ends");
            });

            app.Use(async (context, next) =>
            {
                _logger.LogWarning("\n2nd delegate called");
                
                // Chama o próximo middleware.
                await next();
            });


            // Middlewares nativos.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
