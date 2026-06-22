using API.data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var erro = exceptionFeature?.Error;
        
        string mensagemErro = erro?.Message ?? "Erro inesperado no servidor.";

        if (erro?.InnerException != null)
        {
            string erroBanco = erro.InnerException.Message;

            if (erroBanco.Contains("FOREIGN KEY constraint failed"))
            {
                mensagemErro = "Não é possível deletar este registro pois existem outros registros associados a ele.";
            }
            else if (erroBanco.Contains("NOT NULL constraint failed: Funcionarios.IdCargo"))
            {
                mensagemErro = "O cargo deve ser preenchido!";
            }
            else if (erroBanco.Contains("NOT NULL constraint failed: Cargos.IdArea"))
            {
                mensagemErro = "A area deve ser preenchida!";
            }
            else
            {
                mensagemErro = erroBanco;
            }
        }

        await context.Response.WriteAsJsonAsync(new 
        {
            local = exceptionFeature?.Path,
            erroResumido = mensagemErro
        });
    });
});

app.MapAreaEndpoints();
app.MapCargoEndpoints();
app.MapFuncionarioEndpoints();
app.MapUsuarioEndpoints();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.Run();