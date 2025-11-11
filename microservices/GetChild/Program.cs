using Microsoft.EntityFrameworkCore;
using GetChild.Data;
using GetChild.Models;

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

async Task<List<Child>> GetChilds(DataContext context) => await context.Child.ToListAsync();
app.MapGet("/childs", async (DataContext context) => await GetChilds(context))
.WithName("GetChild")
.WithOpenApi();

app.Run();