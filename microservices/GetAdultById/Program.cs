using Microsoft.EntityFrameworkCore;
using GetAdultById.Models;
using GetAdultById.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

async Task<Adult?> GetAdult(DataContext context, int id) => await context.Adult.FindAsync(id);
app.MapGet("/AdultById/{id}", async (DataContext context, int id) => await GetAdult(context, id))
.WithName("GetAdultById")
.WithOpenApi();

app.Run();