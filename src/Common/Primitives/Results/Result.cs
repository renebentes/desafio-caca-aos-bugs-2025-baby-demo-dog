namespace BugStore.Common.Primitives.Results;

public class Result
{
    protected Result(
        ResultStatus status,
        IEnumerable<Error> errors)
    {
        Status = status;
        Errors = errors;
    }

    private Result(ResultStatus status)
        : this(status, [])
    {
    }

    public IEnumerable<Error> Errors { get; }

    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.Created or ResultStatus.NoContent;

    public ResultStatus Status { get; }

    public static Result Conflict(Error error)
       => new(ResultStatus.Conflict, [error]);

    public static Result Conflict(IEnumerable<Error> errors)
       => new(ResultStatus.Conflict, errors);

    public static Result Created()
       => new(ResultStatus.Created);

    public static Result Failure(Error error)
       => new(ResultStatus.Failure, [error]);

    public static Result Failure(IEnumerable<Error> errors)
       => new(ResultStatus.Failure, errors);

    public static Result Invalid(Error error)
       => new(ResultStatus.Invalid, [error]);

    public static Result Invalid(IEnumerable<Error> errors)
       => new(ResultStatus.Invalid, errors);

    public static Result NoContent()
       => new(ResultStatus.NoContent);

    public static Result NotFound(Error error)
       => new(ResultStatus.NotFound, [error]);

    public static Result Success()
       => new(ResultStatus.Ok);
}
