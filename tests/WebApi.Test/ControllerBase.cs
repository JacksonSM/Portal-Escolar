using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace WebApi.Test;

public class ControllerBase : IClassFixture<PortalEscolarWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(PortalEscolarWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body, string token = "")
    {
        AutorizarRequisicao(token);

        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
    private void AutorizarRequisicao(string token)
    {
        if (!string.IsNullOrWhiteSpace(token) && !_client.DefaultRequestHeaders.Contains("Authorization"))
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }

}
