using System.Net.Http.Headers;
using FluentResults;
using Microsoft.Extensions.Options;
using MonkeyIsland.Application.Configurations;
using MonkeyIsland.Domain.Requests;
using MonkeyIsland.Domain.Responses;
using Newtonsoft.Json;

namespace MonkeyIsland.Application.Services;

public class RequestClient : IRequestClient
{
    public RequestClient(HttpClient httpClient, IOptions<MonkeyIslandConfiguration> monkeyIslandConfiguration)
    {
        _httpClient = httpClient;
        _monkeyIslandConfiguration = monkeyIslandConfiguration.Value;
    }

    private HttpClient _httpClient { get; set; }
    private MonkeyIslandConfiguration _monkeyIslandConfiguration { get; set; }
    
    
    public async Task<Result<ICollection<int>>> GetMagicNumbers()
    {
        var requestUrl = $"{_monkeyIslandConfiguration.ApiUrl}/{_monkeyIslandConfiguration.ApiKey}";
        
        var response = await _httpClient.GetAsync(requestUrl);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail("Could not retrieve magic numbers.");
        }

        try
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<MonkeyIslandMagicNumbersResponse>(responseString);
            
            if (responseJson is null || responseJson.MagicNumbers.Count == 0)
            {
                return Result.Fail("Could not retrieve magic numbers from response.");
            }
            
            return Result.Ok(responseJson.MagicNumbers);
        }
        catch (JsonReaderException)
        {
            return Result.Fail("Response format is incorrect.");
        }
    }
    
    public async Task<Result<string>> GetSecretKey(int calculatedTotal)
    {
        var requestUrl = $"{_monkeyIslandConfiguration.ApiUrl}/{_monkeyIslandConfiguration.ApiKey}";
        var requestData = new MonkeyIslandValidateTotalRequest(calculatedTotal, _monkeyIslandConfiguration.BaseAddress);
        
        var response = await _httpClient.PostAsync(requestUrl, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), new MediaTypeHeaderValue("application/json")));
        
        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail("Could not retrieve secret key, check the calculated total.");
        }

        try
        {
            var responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject<MonkeyIslandValidateTotalResponse>(await response.Content.ReadAsStringAsync());
            
            if (responseJson is null || string.IsNullOrEmpty(responseJson.CallbackUrl))
            {
                return Result.Fail("Could not retrieve secret key from response.");
            }
            
            return Result.Ok(responseJson.CallbackUrl);
        }
        catch (JsonReaderException)
        {
            return Result.Fail("Response format is incorrect.");
        }
    }
    
}