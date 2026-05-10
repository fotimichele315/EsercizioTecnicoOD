 using EsercizioTecnicoOD.Core.Interfaces;

namespace EsercizioTecnicoOD.Infrastructure.Services;

public class RemoteApiService : IRemoteApiService
{
    private readonly HttpClient _httpClient;

    public RemoteApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetXmlAsync(string apiKey)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "http://localhost:5226/api/commesse");

        request.Headers.Add("X-API-KEY", apiKey);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}