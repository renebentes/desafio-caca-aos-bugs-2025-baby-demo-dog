using BugStore.Common.Primitives.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BugStore.Common;

internal static class ResultExtensions
{
    internal static IResult ToProblem(this Result result)
        => result.IsSuccess
        ? throw new InvalidOperationException("Result is successful, cannot convert to problem.")
        : result.Status switch
        {
            ResultStatus.Invalid => TypedResults.ValidationProblem(result.Errors.ToValidationProblemError()),
            ResultStatus.NotFound => result.GenerateProblem(),
            ResultStatus.Conflict => result.GenerateProblem(),
            ResultStatus.Failure => result.GenerateProblem(),
            _ => result.GenerateProblem()
        };

    internal static IDictionary<string, string[]> ToValidationProblemError(this IEnumerable<Error> errors)
        => errors.ToDictionary(
            e => e.Code,
            e => new[] { e.Description });

    internal static Dictionary<string, string[]> ToValidationProblemError(this Error error)
        => new()
        {
            {
                error.Code,
                [error.Description]
            }
        };

    private static ProblemHttpResult GenerateProblem(this Result result)
    {
        return TypedResults.Problem(
            extensions: new Dictionary<string, object?> { { "errors", result.Errors } },
            statusCode: GetStatusCode(result.Status)
            );

        static int GetStatusCode(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid or ResultStatus.Problem => StatusCodes.Status400BadRequest,
                ResultStatus.NotFound => StatusCodes.Status404NotFound,
                ResultStatus.Conflict => StatusCodes.Status409Conflict,
                ResultStatus.Failure => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
