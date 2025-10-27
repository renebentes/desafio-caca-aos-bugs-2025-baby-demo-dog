namespace BugStore.Common.Primitives.Results;

public class Result : Result<Result>
{
    protected Result()
        : base(default!)
    {
    }

    protected Result(ResultStatus status)
        : base(default!, status)
    {
    }

    protected Result(ResultStatus status, IEnumerable<Error> errors)
        : base(default!, status, errors)
    {
    }

    public static Result Created()
        => new(ResultStatus.Created);

    public static Result Failure(Error error)
        => new(ResultStatus.Failure, [error]);

    public static Result Failure(IEnumerable<Error> errors)
       => new(ResultStatus.Failure, errors);

    public static new Result Invalid(Error error)
       => new(ResultStatus.Invalid, [error]);

    public static new Result Invalid(IEnumerable<Error> errors)
        => new(ResultStatus.Invalid, errors);

    public static new Result NoContent()
        => new(ResultStatus.NoContent);

    public static new Result NotFound(Error error)
        => new(ResultStatus.NotFound, [error]);

    public static Result Success()
       => new();
}
