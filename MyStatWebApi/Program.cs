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

app.MapGet("api/v1/user/register/{name}/{surname}/{login}/{password}", async (IUserManager userManager, ISha256Encoder encoder, string name, string surname, string login, string password) => 
    await userManager.AddAsync(new User
{
    Name = name,
    Surname = surname,
    Login = login,
    Password = encoder.ComputeSha256Hash(password)
}));

app.MapGet("api/v1/user/login/{login}/{password}", async (IUserManager userManager, ISha256Encoder encoder, string login, string password) => await userManager.GetByLoginPasswordAsync(login, encoder.ComputeSha256Hash(password)));

app.Run();