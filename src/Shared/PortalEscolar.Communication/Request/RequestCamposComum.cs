namespace PortalEscolar.Communication.Request;
public abstract class  RequestCamposComum : InformacoesPessoais
{
    public string Email { get; set; }
    public string Senha { get; set; }

}
