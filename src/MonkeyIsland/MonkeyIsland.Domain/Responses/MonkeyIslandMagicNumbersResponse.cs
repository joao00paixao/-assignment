using Newtonsoft.Json;

namespace MonkeyIsland.Domain.Responses;

public class MonkeyIslandMagicNumbersResponse
{
    public MonkeyIslandMagicNumbersResponse(ICollection<int> magicNumbers)
    {
        MagicNumbers = magicNumbers;
    }
    
    [JsonProperty("magicNumbers")]
    public ICollection<int> MagicNumbers { get; set; }
}