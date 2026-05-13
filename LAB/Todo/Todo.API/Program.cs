using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen; // Requires the Swashbuckle.AspNetCore NuGet package
using Todo.Infrastructure.Modules;
using Todo.Application.Modules;
using Todo.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoDbContext>(dbOptions =>
{
    dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("cnnStr"));
});

// Add modules
builder.Services.AddInfrastructureModules();
builder.Services.AddApplicationModules();

// Add controllers and API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

// Enable static files (HTML, CSS, JS)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
