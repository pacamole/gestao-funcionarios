using API.data;
using Microsoft.EntityFrameworkCore;
using API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
);

var app = builder.Build();

app.MapAreaEndpoints();
app.MapCargoEndpoints();
app.MapFuncionarioEndpoints();
app.MapUsuarioEndpoints();

app.UseHttpsRedirection();

app.Run();

