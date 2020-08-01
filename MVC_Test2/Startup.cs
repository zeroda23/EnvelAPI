using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MVC_Test2.Entities;
using MVC_Test2.Repository;
using MVC_Test2.Services;

namespace MVC_Test2
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
            // Add framework services.

            Credenciales creds = new Credenciales()
            {
                username = Configuration.GetSection("services:cloudantNoSQLDB:credentials:username").Value,
                password = Configuration.GetSection("services:cloudantNoSQLDB:credentials:password").Value,
                host = Configuration.GetSection("services:cloudantNoSQLDB:credentials:host").Value
            };

            if (creds.username != null && creds.password != null && creds.host != null)
            {
                services.AddAuthorization();
                services
                    .AddSingleton(typeof(Credenciales), creds)
                    .AddTransient<IUserRepository, UserRepository>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<ITipService, TipService>()
                    .AddTransient<ITipRepository, TipRepository>()
                    .AddTransient<IRolRepository, RolRepository>()
                    .AddTransient<IRolService, RolService>()
                    .AddTransient<IPreguntaRepository, PreguntaRepository>()
                    .AddTransient<IPreguntaService, PreguntaService>()
                    .AddTransient<ITestService, TestService>()
                    .AddTransient<LoggingHandler>()
                    .AddHttpClient("cloudant", client =>
                    {
                        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(creds.username + ":" + creds.password));

                        client.BaseAddress = new Uri(Configuration.GetSection("services:cloudantNoSQLDB:credentials:url").Value);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
                    })                   
                    .AddHttpMessageHandler<LoggingHandler>();

                //CloudantStorageClient
                services.AddHttpClient("cloudantStorage", client =>
                {
                    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(creds.username + ":" + creds.password));

                    client.BaseAddress = new Uri(Configuration.GetSection("services:cloudantStorage:credentials:url").Value);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
                });
            }

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddMvc();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cloudantService = ((IUserService)app.ApplicationServices.GetService(typeof(IUserService)));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("{0}\t{1}", request.Method, request.RequestUri);
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine(response.StatusCode);
            return response;
        }
    }
}
