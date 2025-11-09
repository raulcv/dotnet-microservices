using Microsoft.EntityFrameworkCore;
using ApiGlobal.Data;
using ApiGlobal.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlDefaultConnection"));
});
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSqlDefaultConnection")!,name: "PostgreSQL Database Health Check",failureStatus: HealthStatus.Unhealthy);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

async Task<List<Adult>> GetAdults(DataContext context) => await context.Adult.ToListAsync();
app.MapGet("/Adults", async (DataContext context) => await GetAdults(context))
.WithName("GetAdults")
.WithOpenApi();

async Task<List<Child>> GetChildren(DataContext context) => await context.Child.ToListAsync();
app.MapGet("/Child", async (DataContext context) => await GetChildren(context))
.WithName("GetChild")
.WithOpenApi();

app.MapGet("/Adult/{id}", async (DataContext context, int id) => await context.Adult.FindAsync(id))
.WithName("GetAdultById")
.WithOpenApi();

app.MapGet("/Child/{id}", async (DataContext context, int id) => await context.Child.FindAsync(id))
.WithName("GetChildById")
.WithOpenApi();

app.MapPost("Add/Adults",async(DataContext context, Adult item) =>
{
    context.Adult.Add(item);
    await context.SaveChangesAsync();
    return Results.Ok(await GetAdults(context));
})
.WithName("AddAdult")
.WithOpenApi();

app.MapPost("Add/Children",async(DataContext context, Child item) =>
{
    context.Child.Add(item);
    await context.SaveChangesAsync();
    return Results.Ok(await GetChildren(context));
})
.WithName("AddChild")
.WithOpenApi();

app.Run();

