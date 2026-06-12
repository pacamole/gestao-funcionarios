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
            var funcionarios = await db.Funcionarios.ToListAsync();
            funcionarios.ForEach((funcionario) =>
            {
                if (funcionario is not null)
                {
                    var cargo = db.Cargos.Find(funcionario.IdCargo);
                    funcionario.Cargo = cargo;

                    var usuario = db.Usuarios.Find(funcionario.IdUsuario);
                    funcionario.Usuario = usuario;
                }
            });
            return Results.Ok(funcionarios);
        });

        // GET: api/funcionarios/{id}
        group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario is null)
            {
                return Results.NotFound(new { message = "Funcionário não encontrado." });
            }
            var cargo = await db.Cargos.FindAsync(funcionario.IdCargo);
            var usuario = await db.Usuarios.FindAsync(funcionario.IdUsuario);

            funcionario.Cargo = cargo;
            funcionario.Usuario = usuario;
            return Results.Ok(funcionario);
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
            if (funcionario is null)
            {
                return Results.NotFound(new { message = "Funcionário não encontrado." });
            }

            db.Funcionarios.Remove(funcionario);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}