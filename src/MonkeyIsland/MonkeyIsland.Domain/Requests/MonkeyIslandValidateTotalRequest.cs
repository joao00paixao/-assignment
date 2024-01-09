using Newtonsoft.Json;

namespace MonkeyIsland.Domain.Requests;

public class MonkeyIslandValidateTotalRequest
{
    public MonkeyIslandValidateTotalRequest(int sum, string callbackUrl)
    {
        Sum = sum;
        CallbackUrl = callbackUrl;
    }
    
    [JsonProperty("sum")]
    public int Sum { get; set; }
    
    [JsonProperty("callBackUrl")]
    public string CallbackUrl { get; set; }
}