using System;

#nullable disable

namespace Simplify.Web.ModelBinding
{
	/// <summary>
	/// Represent model not supported exceptions
	/// </summary>
	public class ModelNotSupportedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelNotSupportedException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ModelNotSupportedException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}
	}
}