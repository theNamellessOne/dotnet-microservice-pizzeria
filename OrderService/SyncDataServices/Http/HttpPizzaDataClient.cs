using OrderService.Dtos.HttpGetDtos;

namespace OrderService.SyncDataServices.Http;

public class HttpPizzaDataClient : IHttpPizzaDataClient
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public HttpPizzaDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<PizzaHttpGetDto?> GetPizzaById(int id)
    {
        return await Get($"{_configuration["PizzaService"]}/api/pizza/{id}").Result
            .ReadFromJsonAsync<PizzaHttpGetDto>();
    }

    private async Task<HttpContent> Get(string requestUrl)
    {
        Console.WriteLine($"--> Sending Get Request to {requestUrl}");

        var response = await _httpClient.GetAsync(requestUrl);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "--> Sync GET to Service was OK"
            : "--> Sync GET to Service wa NOT OK");

        return response.Content;
    }
}