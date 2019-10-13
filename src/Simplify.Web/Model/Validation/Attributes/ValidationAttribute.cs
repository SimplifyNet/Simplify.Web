using System;
using System.Reflection;
using Simplify.DI;

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
		/// <param name="value">The object value.</param>
		/// <param name="propertyInfo">Information about the property containing this attribute.</param>
		/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
		public abstract void Validate(object value, PropertyInfo propertyInfo, IDIResolver resolver);
	}
}