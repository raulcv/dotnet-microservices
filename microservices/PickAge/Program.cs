using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickAge;


Host.CreateDefaultBuilder(args)
   .ConfigureHostConfiguration(configHost =>
   {
       Console.WriteLine("Logs: appsettings.json loading...");
       configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
   })
   .ConfigureServices((hostContext, services) =>
   {
       IConfiguration configuration = hostContext.Configuration;
       string environmentName = hostContext.HostingEnvironment.EnvironmentName;
       Console.WriteLine($"Running in environment: {environmentName}");
       services.AddOptions();
       services.AddHostedService<Worker>();
   }).Build().Run();