using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseNpgsql(builder.Configuration["database:PostgreSQL"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(BancoPost.Caminho, BancoPost.Metodo, BancoPost.Comportamento);
app.MapMethods(BoletoPost.Caminho, BoletoPost.Metodo, BoletoPost.Comportamento);


app.Run();