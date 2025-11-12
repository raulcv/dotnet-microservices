using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AddAdult;
using AddAdult.Data;

IServiceCollection serviceDescriptors = new ServiceCollection();

Host.CreateDefaultBuilder(args)
   .ConfigureHostConfiguration(configHost =>
   {
       configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
   })
   .ConfigureServices((hostContext, services) =>
   {
    string connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")!;
       IConfiguration configuration = hostContext.Configuration;
       string environmentName = hostContext.HostingEnvironment.EnvironmentName;
       Console.WriteLine($"Running in environment^_^: {environmentName}");
       services.AddOptions();
       services.AddHostedService<Worker>();
       services.AddDbContext<DataContext>(options =>
           options.UseNpgsql(connectionString));
   }).Build().Run();