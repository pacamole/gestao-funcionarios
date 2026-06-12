using Microsoft.EntityFrameworkCore;
using API.models;
using API.data;

namespace API.Endpoints;

public static class CargoEndpoints
{
    public static void MapCargoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargos").WithTags("Cargos");

        // GET: api/cargos
        group.MapGet("/", async (AppDbContext db) =>
        {
            var cargos = await db.Cargos.ToListAsync();

            cargos.ForEach((cargo) =>
            {
                var area = db.Areas.Find(cargo.IdArea);
                cargo.Area = area;
            });
            return Results.Ok(cargos);
        });

        // GET: api/cargos/{id}
        group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var cargo = await db.Cargos.FindAsync(id);
            if (cargo is null)
            {
                return Results.NotFound(new { message = "Cargo não encontrado." });
            }
            
            var area = await db.Areas.FindAsync(cargo.IdArea);
            cargo.Area = area;

            return Results.Ok(cargo);
        });

        // POST: api/cargos
        group.MapPost("/", async (Cargo cargo, AppDbContext db) =>
        {
            db.Cargos.Add(cargo);
            await db.SaveChangesAsync();
            return Results.Created($"/api/cargos/{cargo.Id}", cargo);
        });

        // PUT: api/cargos/{id}
        group.MapPut("/{id:guid}", async (Guid id, Cargo inputCargo, AppDbContext db) =>
        {
            if (id != inputCargo.Id) return Results.BadRequest(new { message = "O ID da URL não corresponde ao ID do cargo." });

            var cargo = await db.Cargos.FindAsync(id);
            if (cargo is null) return Results.NotFound(new { message = "Cargo não encontrado." });

            cargo.Nome = inputCargo.Nome;
            cargo.Salario = inputCargo.Salario;
            cargo.IdArea = inputCargo.IdArea;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE: api/cargos/{id}
        group.MapDelete("/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var cargo = await db.Cargos.FindAsync(id);
            if (cargo is null) return Results.NotFound(new { message = "Cargo não encontrado." });

            db.Cargos.Remove(cargo);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}