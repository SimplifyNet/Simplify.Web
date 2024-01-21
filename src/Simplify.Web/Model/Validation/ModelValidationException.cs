using System;

namespace Simplify.Web.Model.Validation;

/// <summary>
/// Represent model validation exceptions.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ModelValidationException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
public class ModelValidationException(string message, Exception? innerException = null) : Exception(message, innerException)
{
}