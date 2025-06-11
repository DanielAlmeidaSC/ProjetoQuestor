using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Questor API - Avaliação",
        Version = "v1",
        Description = "API para gerenciamento de bancos, juros e boletos. Desenvolvido com ASP.NET Core 6 utilizando Minimal API"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration["database:PostgreSQL"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Documentação da API Questor - Avaliação";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Questor API v1");
        options.RoutePrefix = "swagger"; 
        options.DisplayRequestDuration(); 

    });
}

app.UseHttpsRedirection();

app.MapMethods(BancoPost.Caminho, BancoPost.Metodo, BancoPost.Comportamento)
    .WithTags("Banco Post");

app.MapMethods(BoletoPost.Caminho, BoletoPost.Metodo, BoletoPost.Comportamento)
    .WithTags("Boleto Post");

app.MapMethods(TodosBancosGet.Caminho, TodosBancosGet.Metodo, TodosBancosGet.Comportamento)
    .WithTags("Todos os Bancos");

app.MapMethods(IdBancoGet.Caminho, IdBancoGet.Metodo, IdBancoGet.Comportamento)
    .WithTags("Banco por Id");

app.MapMethods(BoletoIdGet.Caminho, BoletoIdGet.Metodo, BoletoIdGet.Comportamento)
    .WithTags("Boleto por Id");

app.MapMethods(TodosBoletosGet.Caminho, TodosBoletosGet.Metodo, TodosBoletosGet.Comportamento)
    .WithTags("Todos os boletos");


app.Run();