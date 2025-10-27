using BugStore.Common.Primitives.Results;
using System.Text;

namespace BugStore.Common;

internal static class ResultExtensions
{
    internal static IResult Map<TOut>(this Result<TOut> result, string route = "")
        => result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            ResultStatus.Created => Results.Created($"{route}", result.Value),
            ResultStatus.NoContent => Results.NoContent(),
            ResultStatus.Invalid or ResultStatus.Problem => Results.BadRequest(result.GenerateProblem),
            ResultStatus.NotFound => Results.NotFound(result.GenerateProblem),
            ResultStatus.Conflict => Results.Conflict(result.GenerateProblem),
            ResultStatus.Failure => Results.UnprocessableEntity(result.GenerateProblem),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };

    private static IResult GenerateProblem<TOut>(this Result<TOut> result)
    {
        return Results.Problem(
            detail: GetDetails(result.Errors),
            statusCode: GetStatusCode(result.Status),
            title: GetTitle(result.Status),
            type: GetType(result.Status));

        static string GetTitle(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid => "Invalid Request.",
                ResultStatus.Problem => "Problem Request.",
                ResultStatus.NotFound => "Resource Not Found.",
                ResultStatus.Conflict => "There was a conflict.",
                ResultStatus.Failure => "Operation Failed.",
                _ => "Something went wrong."
            };

        static int GetStatusCode(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid or ResultStatus.Problem => StatusCodes.Status400BadRequest,
                ResultStatus.NotFound => StatusCodes.Status404NotFound,
                ResultStatus.Failure => StatusCodes.Status422UnprocessableEntity,
                ResultStatus.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static string GetType(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ResultStatus.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ResultStatus.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ResultStatus.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                ResultStatus.Failure => "https://tools.ietf.org/html/rfc4918#section-11.2",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static string GetDetails(IEnumerable<Error> errors)
        {
            var details = new StringBuilder("Next status(s) occurred:");
            foreach (var error in errors)
            {
                details
                    .Append("- ")
                    .AppendLine(error.ToString());
            }

            return details.ToString();
        }
    }
}
