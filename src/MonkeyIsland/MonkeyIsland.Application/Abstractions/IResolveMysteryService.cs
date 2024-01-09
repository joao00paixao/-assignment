using FluentResults;

namespace MonkeyIsland.Application.Abstractions;

public interface IResolveMysteryService
{
    Task<Result<string>> ResolveMystery();
}