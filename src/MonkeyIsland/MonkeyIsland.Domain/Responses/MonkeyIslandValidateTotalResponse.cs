using Newtonsoft.Json;

namespace MonkeyIsland.Domain.Responses;

public class MonkeyIslandValidateTotalResponse
{
    public MonkeyIslandValidateTotalResponse(string callbackUrl)
    {
        CallbackUrl = callbackUrl;
    }
    
    [JsonProperty("callBackUrl")]
    public string CallbackUrl { get; set; }
}