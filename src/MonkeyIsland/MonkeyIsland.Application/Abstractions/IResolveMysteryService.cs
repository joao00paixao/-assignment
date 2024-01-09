using FluentResults;

namespace MonkeyIsland.Application.Abstractions;

public interface IResolveMysteryService
{
    Task<Result<bool>> ResolveMystery();
}