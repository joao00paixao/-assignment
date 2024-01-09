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
    
    public async Task<Result<bool>> ResolveMystery()
    {
        var magicNumbers = await _requestClient.GetMagicNumbers();
        
        if (!magicNumbers.IsSuccess)
        {
            return Result.Fail(string.Empty);
        }
        
        var calculatedResult = magicNumbers.Value.Sum();
        
        var resolveResult = await _requestClient.ValidateCalculatedTotal(calculatedResult);
        
        return resolveResult.IsSuccess;
    }
}