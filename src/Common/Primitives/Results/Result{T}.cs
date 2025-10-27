using System.Diagnostics.CodeAnalysis;

namespace BugStore.Common.Primitives.Results;

public class Result<TValue>
{
    public Result(TValue value)
        => Value = value;

    protected Result(
        TValue value,
        ResultStatus status)
        : this(value)
        => Status = status;

    protected Result(
        TValue value,
        ResultStatus status,
        IEnumerable<Error> errors)
        : this(value, status)
        => Errors = errors;

    public IEnumerable<Error> Errors { get; } = [];

    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.Created or ResultStatus.NoContent;

    public ResultStatus Status { get; } = ResultStatus.Ok;

    [NotNull]
    public TValue Value
    {
        get => IsSuccess
        ? field!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");
    }

    public static Result<TValue> Created(TValue value)
        => new(value, ResultStatus.Created);

    public static Result<TValue> Invalid(Error error)
        => new(default!, ResultStatus.Invalid, [error]);

    public static Result<TValue> Invalid(IEnumerable<Error> errors)
        => new(default!, ResultStatus.Invalid, errors);

    public static Result<TValue> NoContent()
        => new(default!, ResultStatus.NoContent);

    public static Result<TValue> NotFound(Error error)
        => new(default!, ResultStatus.NotFound, [error]);

    public static Result<TValue> Success(TValue value)
       => new(value, ResultStatus.Ok);

    public static implicit operator Result<TValue>(TValue value)
       => Success(value);

    public static implicit operator Result<TValue>(Result result)
       => new(default!, result.Status, result.Errors);
}
