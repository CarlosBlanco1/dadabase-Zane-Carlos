using dadabase.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJokeStore, PostgresDataStore>();//scoped not singleton

builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true); //add secretfile
builder.Services.AddDbContext<JokeContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:Dadabase"]);
});
//builder.Services.AddHostedService<MigrationService>();

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

app.Run();
