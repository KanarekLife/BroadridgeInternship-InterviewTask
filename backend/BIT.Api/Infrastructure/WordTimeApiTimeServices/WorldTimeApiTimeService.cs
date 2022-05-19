using System.Text.Json;
using System.Text.Json.Serialization;
using BIT.Api.Services;

namespace BIT.Api.Infrastructure.WordTimeApiTimeServices;

public class WorldTimeApiTimeService : ITimeService
{
    private readonly string _timezone;
    private readonly HttpClient _client;

    private const string Url = "https://worldtimeapi.org/api";

    public WorldTimeApiTimeService(IConfiguration configuration)
    {
        _timezone = configuration["Timezone"];
        _client = new HttpClient();
    }
    
    public async Task<DateTime> GetCurrentTime()
    {
        if (_timezone.Equals(string.Empty))
        {
            throw new WorldTimeApiInvalidTimezoneException(string.Empty, await GetValidTimezones());
        }
        
        var response = await _client.GetAsync($"{Url}/timezone/{_timezone}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<WorldTimeApiErrorDto>();
            if (content is not null && content.ErrorDetails.StartsWith("unknown location"))
            {
                throw new WorldTimeApiInvalidTimezoneException(_timezone, await GetValidTimezones());
            }
            throw new WorldTimeApiReturnsErrorException(content?.ErrorDetails);
        }

        var dto = await JsonSerializer.DeserializeAsync<WorldTimeApiDto>(await response.Content.ReadAsStreamAsync());
        if (dto is null)
        {
            throw new WorldTimeApiInvalidResponseException();
        }
        
        return dto.DateTime;
    }

    private async Task<string[]> GetValidTimezones()
    {
        var response = await _client.GetAsync($"{Url}/timezone/");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<WorldTimeApiErrorDto>();
            throw new WorldTimeApiReturnsErrorException(content?.ErrorDetails);
        }

        var dto = await response.Content.ReadFromJsonAsync<string[]>();
        if (dto is null)
        {
            throw new WorldTimeApiInvalidResponseException();
        }

        return dto;
    }

    private class WorldTimeApiDto
    {
        [JsonPropertyName("datetime")]
        public DateTime DateTime { get; set; }
    }

    private class WorldTimeApiErrorDto
    {
        [JsonPropertyName("error")]
        public string ErrorDetails { get; set; }
    }
}