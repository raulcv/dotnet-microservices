using Microsoft.EntityFrameworkCore;
using GetChildById.Data;
using GetChildById.Models;

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

async Task<Child?> GetChildById(DataContext context, int id) => await context.Child.FirstOrDefaultAsync(x => x.Id == id);
app.MapGet("/ChildById/{id}", async (DataContext context, int id) => await GetChildById(context, id))
.WithName("GetChildById")
.WithOpenApi();

app.Run();