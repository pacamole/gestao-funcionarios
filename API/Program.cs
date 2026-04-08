using API.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Ops! Parece que não tem nada ainda! <br>hihihi");

app.Run();

