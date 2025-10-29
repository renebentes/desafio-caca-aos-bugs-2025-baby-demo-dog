using System.Diagnostics.CodeAnalysis;

namespace BugStore.Common.Primitives.Results;

public sealed class Result<TValue> : Result
{
    private Result(
        TValue? value,
        ResultStatus status)
        : this(value, status, [])
    {
    }

    private Result(
        TValue? value,
        ResultStatus status,
        IEnumerable<Error> errors)
        : base(status, errors)
    {
        Value = value;
    }

    [NotNull]
    public TValue Value
    {
        get => IsSuccess
        ? field!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");
    }

    public static new Result<TValue> Conflict(Error error)
        => new(default!, ResultStatus.Conflict, [error]);

    public static new Result<TValue> Conflict(IEnumerable<Error> errors)
        => new(default!, ResultStatus.Conflict, errors);

    public static Result<TValue> Created(TValue value)
        => new(value, ResultStatus.Created);

    public static new Result<TValue> Failure(IEnumerable<Error> errors)
       => new(default!, ResultStatus.Failure, errors);

    public static new Result<TValue> Failure(Error error)
       => new(default!, ResultStatus.Invalid, [error]);

    public static new Result<TValue> Invalid(Error error)
        => new(default!, ResultStatus.Invalid, [error]);

    public static new Result<TValue> Invalid(IEnumerable<Error> errors)
        => new(default!, ResultStatus.Invalid, errors);

    public static new Result<TValue> NoContent()
        => new(default!, ResultStatus.NoContent);

    public static new Result<TValue> NotFound(Error error)
        => new(default!, ResultStatus.NotFound, [error]);

    public static Result<TValue> Success(TValue value)
       => new(value, ResultStatus.Ok);

    public static implicit operator Result<TValue>(TValue value)
       => Success(value);
}
