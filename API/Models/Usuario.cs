namespace API.models;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Permissoes { get; set; } = "PADRAO";    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}