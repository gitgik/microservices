using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platformReadDto),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("http://localhost:6000/api/c/platforms", httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Sync to command service Ok!");
        }
        else
        {
            Console.WriteLine("Sync to command service failed");
        }
    }

}