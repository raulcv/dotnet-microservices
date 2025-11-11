using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickAge;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IServiceCollection serviceDescriptors = new ServiceCollection();

Host.CreateDefaultBuilder(args)
   .ConfigureHostConfiguration(configHost =>
   {
       if (app.Environment.IsDevelopment())
       {
           configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
           Console.WriteLine("Development, development is enabled: {0}", app.Environment.IsDevelopment());
       }else
       {
           Console.WriteLine("Production, development is avoided: {0}", app.Environment.IsDevelopment());
       }
   })
   .ConfigureServices((hostContext, services) =>
   {
       IConfiguration configuration = hostContext.Configuration;
       services.AddOptions();
       services.AddHostedService<Worker>();
   }).Build().Run();