using Microsoft.EntityFrameworkCore;
using TodoApp.Server.Config.AutoMapper;
using TodoApp.Server.Data;
using TodoApp.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddDbContext<TodoAppDbContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
                    .WithOrigins("https://localhost:7267")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true)
        );
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
