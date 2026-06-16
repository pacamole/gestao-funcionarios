namespace API.models;

using System.ComponentModel.DataAnnotations.Schema;

public class Funcionario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Modalidade { get; set; } = "CLT";
    public string? Observacoes { get; set; }
    public DateTime ValidadeContrato { get; set; }
    public Guid? IdCargo { get; set; }

    [ForeignKey("IdCargo")] 
    public Cargo? Cargo { get; set; }
   
    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdUsuario")] 
    public Usuario? Usuario { get; set; }


}