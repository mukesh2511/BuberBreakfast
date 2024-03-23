using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);

// Configure services.
builder.Services.AddControllers();
builder.Services.AddScoped<IBreakfastService, BreakfastService>(); // Register IBreakfastService

var app = builder.Build();

app.UseExceptionHandler("/error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // Enable HTTPS redirection only in non-development environment
}

app.MapControllers(); // Map controllers to routes

app.Run();
