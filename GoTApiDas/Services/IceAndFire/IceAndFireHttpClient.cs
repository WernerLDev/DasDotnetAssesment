using System.Text.Json;
using GoTApiDas.Models.IceOfFireApi;

namespace GoTApiDas.Services.IceAndFire;

public class IceAndFireHttpClient : IIceAndFireHttpClient
{
  private readonly HttpClient _httpClient;
  private readonly JsonSerializerOptions _jsonSerializerOptions;

  public IceAndFireHttpClient()
  {
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri("https://www.anapioficeandfire.com");

    _jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
  }

  public async Task<List<ApiCharacter>?> FetchCharacters()
  {
    var response = await _httpClient.GetAsync("/api/characters");
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<List<ApiCharacter>>(responseBody, _jsonSerializerOptions);
  }
}
