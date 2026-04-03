namespace API.models;

public class Area
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Classificacao { get; set; } = String.Empty;
    public Guid? IdResponsavel { get; set; }
    public Guid? IdAreaPai { get; set; }

    public ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}