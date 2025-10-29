using BugStore.Common.Primitives.Results;
using FluentValidation.Results;
using System;

namespace BugStore.Common;

internal static class FluentValidationExtensions
{
    internal static IEnumerable<Error> AsErrors(this ValidationResult validationResult)
    {
        List<Error> errors = [];

        validationResult.Errors.ForEach(failure =>
            errors.Add(
                new Error(
                    $"Validation.{failure.PropertyName}.{failure.ErrorCode}",
                    failure.ErrorMessage
                )
            )
        );

        return errors;
    }
}
