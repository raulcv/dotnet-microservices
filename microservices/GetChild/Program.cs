using Microsoft.EntityFrameworkCore;
using GetChild.Data;
using GetChild.Models;

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

async Task<List<Child>> GetChilds(DataContext context) => await context.Child.ToListAsync();
app.MapGet("/childs", async (DataContext context) => await GetChilds(context))
.WithName("GetChild")
.WithOpenApi();

app.Run();