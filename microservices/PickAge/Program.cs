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
    if (app.Environment.IsDevelopment()){
           configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
       }
   })
   .ConfigureServices((hostContext, services) =>
   {
       IConfiguration configuration = hostContext.Configuration;
       services.AddOptions();
       services.AddHostedService<Worker>();
   }).Build().Run();