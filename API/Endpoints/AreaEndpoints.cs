using Microsoft.EntityFrameworkCore;
using API.models;
using API.data;

namespace API.Endpoints;

public static class AreaEndpoints
{
    public static void MapAreaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/areas").WithTags("Areas");

        // GET: api/areas
        group.MapGet("/", async (AppDbContext db) =>
        {
            return Results.Ok(await db.Areas.ToListAsync());
        });

        // GET: api/areas/{id}
        group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var area = await db.Areas.FindAsync(id);
            return area is not null ? Results.Ok(area) : Results.NotFound(new { message = "Área não encontrada." });
        });

        // POST: api/areas
        group.MapPost("/", async (Area area, AppDbContext db) =>
        {
            db.Areas.Add(area);
            await db.SaveChangesAsync();
            return Results.Created($"/api/areas/{area.Id}", area);
        });

        // PUT: api/areas/{id}
        group.MapPut("/{id:guid}", async (Guid id, Area inputArea, AppDbContext db) =>
        {
            if (id != inputArea.Id) return Results.BadRequest(new { message = "O ID da URL não corresponde ao ID da área." });

            var area = await db.Areas.FindAsync(id);
            if (area is null) return Results.NotFound(new { message = "Área não encontrada." });

            area.Nome = inputArea.Nome;
            area.Classificacao = inputArea.Classificacao;
            area.IdResponsavel = inputArea.IdResponsavel;
            area.IdAreaPai = inputArea.IdAreaPai;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE: api/areas/{id}
        group.MapDelete("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var area = await db.Areas.FindAsync(id);
            if (area is null) return Results.NotFound(new { message = "Área não encontrada." });

            db.Areas.Remove(area);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}