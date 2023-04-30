using System;

#nullable disable

namespace Simplify.Web.ModelBinding;

/// <summary>
/// Represent model binding exceptions
/// </summary>
public class ModelBindingException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ModelBindingException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
	public ModelBindingException(string message, Exception innerException = null) : base(message, innerException)
	{
	}
}