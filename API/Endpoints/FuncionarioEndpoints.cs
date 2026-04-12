using Microsoft.EntityFrameworkCore;
using API.models;
using API.data; 

namespace API.Endpoints;

public static class FuncionarioEndpoints
{
    public static void MapFuncionarioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/funcionarios").WithTags("Funcionarios");

        // GET: api/funcionarios
        group.MapGet("/", async (AppDbContext db) =>
        {
            return Results.Ok(await db.Funcionarios.ToListAsync());
        });

        // GET: api/funcionarios/{id}
        group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            return funcionario is not null ? Results.Ok(funcionario) : Results.NotFound(new { message = "Funcionário não encontrado." });
        });

        // POST: api/funcionarios
        group.MapPost("/", async (Funcionario funcionario, AppDbContext db) =>
        {
            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();
            return Results.Created($"/api/funcionarios/{funcionario.Id}", funcionario);
        });

        // PUT: api/funcionarios/{id}
        group.MapPut("/{id:guid}", async (Guid id, Funcionario inputFuncionario, AppDbContext db) =>
        {
            if (id != inputFuncionario.Id) return Results.BadRequest(new { message = "O ID da URL não corresponde ao ID do funcionário." });

            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario is null) return Results.NotFound(new { message = "Funcionário não encontrado." });

            funcionario.Nome = inputFuncionario.Nome;
            funcionario.Modalidade = inputFuncionario.Modalidade;
            funcionario.Observacoes = inputFuncionario.Observacoes;
            funcionario.ValidadeContrato = inputFuncionario.ValidadeContrato;
            funcionario.IdCargo = inputFuncionario.IdCargo;
            funcionario.IdUsuario = inputFuncionario.IdUsuario;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE: api/funcionarios/{id}
        group.MapDelete("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario is null) return Results.NotFound(new { message = "Funcionário não encontrado." });

            db.Funcionarios.Remove(funcionario);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}