using Biblioteca.Interfaces;
using Biblioteca.Services;
using Bibliote.Interface;
using Bibliote.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our services
builder.Services.AddScoped<IAutorService, AutorFileService>();
builder.Services.AddScoped<ILibroService, LibroFileService>();
builder.Services.AddSingleton<IPersonaService, PersonaMemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
