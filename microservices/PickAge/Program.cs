using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickAge;

IServiceCollection serviceDescriptors = new ServiceCollection();

Host.CreateDefaultBuilder(args)
   .ConfigureHostConfiguration(configHost =>
   {
       // Only load appsettings.json in development
       if (!args.Contains("--production"))
       {
           configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
           Console.WriteLine("Development mode enabled :)");
       }
       else
       {
           Console.WriteLine("Production mode enabled ;)");
       }
   })
   .ConfigureServices((hostContext, services) =>
   {
       IConfiguration configuration = hostContext.Configuration;
       services.AddOptions();
       services.AddHostedService<Worker>();
   }).Build().Run();