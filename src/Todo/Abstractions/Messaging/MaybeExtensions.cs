using Todo.Core.Primitives.Maybe;

namespace Todo.Abstractions.Messaging;

public static class MaybeExtensions
{
    public static async Task<IResult> Match<TIn>(
        this Task<Maybe<TIn>> resultTask,
        Func<TIn, IResult> onSuccess,
        Func<IResult> onFailure)
    {
        Maybe<TIn> maybe = await resultTask;

        return maybe.HasValue ? onSuccess(maybe.Value) : onFailure();
    }
    
}