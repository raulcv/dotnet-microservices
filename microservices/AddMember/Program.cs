using AddMember.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/addmember", (string name, string LastName, string birthyear) =>
{
    string connectionString = builder.Configuration["ServiceBus:ConnectionString"] ?? Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION_STRING")!;
    string queueName = builder.Configuration["ServiceBus:QueueName"] ?? Environment.GetEnvironmentVariable("SERVICEBUS_QUEUE_NAME")!;

    Console.WriteLine("ASB ConnectionString is null or empty: {0}, QueueName is null or empty: {1}", string.IsNullOrEmpty(connectionString), string.IsNullOrEmpty(queueName)); // var serviceBus = new ServiceBus(connectionString, queueName);
    
    var serviceBus = new ServiceBus(connectionString, queueName);
    serviceBus.SendMessageAsync(name, LastName, birthyear).GetAwaiter().GetResult();
    return Results.Ok($"Miembro {name} agregado con éxito.");
})
.WithName("AddMember")
.WithOpenApi();

app.Run();