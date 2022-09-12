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

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {

        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }


}
