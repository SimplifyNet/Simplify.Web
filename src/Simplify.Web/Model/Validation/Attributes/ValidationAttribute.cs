using System;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Represent model property validation attribute base class
	/// </summary>
	/// <seealso cref="Attribute" />
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class ValidationAttribute : Attribute
	{
		/// <summary>
		/// Validates the specified property value.
		/// </summary>
		/// <param name="value">The value.</param>
		public abstract void Validate(object value);
	}
}