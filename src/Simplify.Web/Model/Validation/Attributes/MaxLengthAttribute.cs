using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Sets maximum required property length
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MaxLengthAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaxLengthAttribute" /> class.
		/// </summary>
		/// <param name="maximumPropertyLength">Maximum length of the property.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
		public MaxLengthAttribute(int maximumPropertyLength,
			string? errorMessage = null,
			bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable)
		{
			MaximumPropertyLength = maximumPropertyLength;
		}

		/// <summary>
		/// Gets or sets the maximum length of the property.
		/// </summary>
		/// <value>
		/// The maximum length of the property.
		/// </value>
		public int MaximumPropertyLength { get; set; }

		/// <summary>
		/// Validates the specified property value.
		/// </summary>
		/// <param name="value">The object value.</param>
		/// <param name="propertyInfo">Information about the property containing this attribute.</param>
		/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
		public override void Validate(object? value, PropertyInfo propertyInfo, IDIResolver resolver)
		{
			if (value is not string s)
				return;

			if (s.Length <= MaximumPropertyLength)
				return;

			TryThrowCustomOrStringTableException(resolver);

			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required maximum length is '{MaximumPropertyLength}', actual value: '{s}'");
		}
	}
}