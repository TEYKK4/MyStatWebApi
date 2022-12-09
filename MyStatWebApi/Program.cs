using MyStatWebApi.Services;
using Microsoft.EntityFrameworkCore;
using MyStatWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyStatDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyStatDb"));
});
builder.Services.AddScoped<IHomeworkManager, HomeworkManager>();
builder.Services.AddScoped<ISha256Encoder, Sha256Encoder>();
builder.Services.AddScoped<IUserManager, UserManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("api/v1/user/register/{name}/{surname}/{login}/{password}", async (IUserManager userManager, ISha256Encoder encoder, string name, string surname, string login, string password) =>
    {
        if (await userManager.AddAsync(new User
            {
                Name = name,
                Surname = surname,
                Login = login,
                Password = encoder.ComputeSha256Hash(password)
            }))
        {
            return Results.Ok();
        }
        return Results.BadRequest();
    });

app.MapPost("api/v1/user/remove/{id:int}", async (IUserManager userManager, int id) =>
{
    if (await userManager.RemoveAsync(id))
    {
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.MapGet("api/v1/user/get/{id:int}", async (IUserManager userManager, int id) =>
{
    if (await userManager.GetByIdAsync(id) is { } user)
    {
        return Results.Json(user);
    }
    return Results.BadRequest();
});

app.MapGet("api/v1/user/login/{login}/{password}", async (IUserManager userManager, ISha256Encoder encoder, string login, string password) =>
{
    if (await userManager.GetByLoginPasswordAsync(login, encoder.ComputeSha256Hash(password)) is { } user)
    {
        return Results.Json(user);
    }
    return Results.BadRequest();
});

app.MapPost("api/v1/homework/add/{userId:int}/{description}", async (IHomeworkManager homeworkManager, int userId, string description) =>
{
    if (await homeworkManager.AddAsync(new Homework
        {
            Description = description,
            UserId = userId,
            DateTime = DateTime.Now
        }))
    {
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.MapPost("api/v1/homework/remove/{id:int}", async (IHomeworkManager homeworkManager, int id) =>
{
    if (await homeworkManager.RemoveAsync(id))
    {
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.MapGet("api/v1/homework/get/{id:int}", async (IHomeworkManager homeworkManager, int id) =>
{
    if (await homeworkManager.GetByIdAsync(id) is { } homework)
    {
        return Results.Json(homework);
    }
    return Results.BadRequest();
});

app.Run();