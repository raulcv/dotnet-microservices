using Microsoft.EntityFrameworkCore;
using GetAdult.Data;
using GetAdult.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")!;
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

async Task<List<Adult>> GetAdults(DataContext context) => await context.Adult.ToListAsync();
app.MapGet("/Adult", async (DataContext context) => await GetAdults(context))
.WithName("GetAdult")
.WithOpenApi();

app.Run();