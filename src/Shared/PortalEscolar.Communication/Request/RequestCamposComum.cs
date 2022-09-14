namespace PortalEscolar.Communication.Request;
public abstract class  RequestCamposComum
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string NomeCompleto { get; set; }
    public string DataNascimento { get; set; }
}
