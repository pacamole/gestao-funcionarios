namespace API.models;

using System.Text.Json.Serialization;

public class Cargo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public decimal Salario { get; set; }
    public Guid IdArea { get; set; }
    
    // Propriedade para fins de navegação
    public Area? Area { get; set; }

    [JsonIgnore]
    public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}