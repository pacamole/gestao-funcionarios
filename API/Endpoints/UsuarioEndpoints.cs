using Microsoft.EntityFrameworkCore;
using API.models;
using API.data;

namespace API.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usuarios").WithTags("Usuarios");

        // GET: api/usuarios
        group.MapGet("/", async (AppDbContext db) =>
        {
            return Results.Ok(await db.Usuarios.ToListAsync());
        });

        // GET: api/usuarios/{id}
        group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var usuario = await db.Usuarios.FindAsync(id);
            return usuario is not null ? Results.Ok(usuario) : Results.NotFound(new { message = "Usuário não encontrado." });
        });

        // POST: api/usuarios
        group.MapPost("/", async (Usuario usuario, AppDbContext db) =>
        {
            // Nota de Segurança: Em um projeto real, nunca salve a senha em texto limpo.
            // Utilize uma biblioteca como BCrypt.Net-Next ou o PasswordHasher do Identity para criptografar (hash) a senha antes de salvar.
            
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();
            return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
        });

        // PUT: api/usuarios/{id}
        group.MapPut("/{id:guid}", async (Guid id, Usuario inputUsuario, AppDbContext db) =>
        {
            if (id != inputUsuario.Id) return Results.BadRequest(new { message = "O ID da URL não corresponde ao ID do usuário." });

            var usuario = await db.Usuarios.FindAsync(id);
            if (usuario is null) return Results.NotFound(new { message = "Usuário não encontrado." });

            usuario.Email = inputUsuario.Email;
            usuario.Senha = inputUsuario.Senha; // Novamente, aplique o Hash aqui se estiver alterando a senha em um projeto real.
            usuario.Permissoes = inputUsuario.Permissoes;
            // Geralmente, a DataCriacao não deve ser alterada em um update.

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE: api/usuarios/{id}
        group.MapDelete("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var usuario = await db.Usuarios.FindAsync(id);
            if (usuario is null) return Results.NotFound(new { message = "Usuário não encontrado." });

            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}