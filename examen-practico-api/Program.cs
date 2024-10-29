using examen_practico_api.Data;
using examen_practico_api.Data.Interfaces;
using examen_practico_api.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Singleton para conexión con base de datos
builder.Services.AddSingleton(SqlConnectionSingleton.Instance);
builder.Services.AddScoped<IProductoService, ProductoService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
