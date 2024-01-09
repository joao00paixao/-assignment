using FluentResults;
using MonkeyIsland.Application.Abstractions;

namespace MonkeyIsland.Application.Services;

public class ResolveMysteryService : IResolveMysteryService
{

    private IRequestClient _requestClient { get; set; }

    public ResolveMysteryService(IRequestClient requestClient)
    {
        _requestClient = requestClient;
    }
    
    public async Task<Result<string>> ResolveMystery()
    {
        var magicNumbers = await _requestClient.GetMagicNumbers();
        
        if (!magicNumbers.IsSuccess)
        {
            return Result.Fail(string.Empty);
        }
        
        var calculatedResult = magicNumbers.Value.Sum();
        
        var secretKey = await _requestClient.GetSecretKey(calculatedResult);
        
        return !secretKey.IsSuccess ? Result.Fail(string.Empty) : secretKey;
    }
}