namespace BugStore.Common.Primitives.Results;

public enum ResultStatus
{
    Ok = 0,
    Created = 1,
    NoContent = 2,
    Failure = 3,
    Invalid = 4,
    Problem = 5,
    NotFound = 6,
    Conflict = 7
}