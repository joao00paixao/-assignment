using FluentResults;

namespace MonkeyIsland.Application.Services;

public interface IRequestClient
{
    Task<Result<ICollection<int>>> GetMagicNumbers();
    Task<Result<string>> GetSecretKey(int calculatedTotal);
}