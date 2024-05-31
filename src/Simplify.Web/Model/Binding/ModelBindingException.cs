using System;

namespace Simplify.Web.Model.Binding;

/// <summary>
/// Provides the model binding exceptions.
/// </summary>
/// <seealso cref="Exception" />
/// <remarks>
/// Initializes a new instance of the <see cref="ModelBindingException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
public class ModelBindingException(string message, Exception? innerException = null) : Exception(message, innerException);