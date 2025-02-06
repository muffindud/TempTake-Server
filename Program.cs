using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;


int serverPort = int.Parse(Environment.GetEnvironmentVariable("SERVER_PORT") ?? "8080");
string dbServer = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "temptake";
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "posgres";
string connectionString = $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};Database={Environment.GetEnvironmentVariable("DB_NAME")};User Id={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.WebHost.UseUrls($"http://*:{serverPort}");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
