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
        try
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localhost:5226/api/commesse");

            request.Headers.Add("X-API-KEY", apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                throw new Exception(
                    $"Errore chiamata API remota. Status code: {statusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        } catch (HttpRequestException ex)
        {
            throw new Exception(
                "Errore di comunicazione con la API remota.",
                ex);
        } catch (TaskCanceledException ex)
        {
            throw new Exception(
                "Timeout nella chiamata alla API remota.",
                ex);
        }
    }
}