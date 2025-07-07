using gfcf14_art_backend.Data;
using gfcf14_art_backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ArtworkService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["AppSettings:DB_URL"]));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enables CORS for the url of the deployed/local frontend instance
var frontendOrigin = builder.Configuration["AppSettings:FRONT_END_ORIGIN"]!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(frontendOrigin).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendPolicy");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
