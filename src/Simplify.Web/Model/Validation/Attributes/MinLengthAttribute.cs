#nullable disable

using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Sets minimum required property length
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MinLengthAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MinLengthAttribute" /> class.
		/// </summary>
		/// <param name="minimumPropertyLength">Minimum length of the property.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
		public MinLengthAttribute(int minimumPropertyLength,
			string errorMessage = null,
			bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable)
		{
			MinimumPropertyLength = minimumPropertyLength;
		}

		/// <summary>
		/// Gets or sets the minimum length of the property.
		/// </summary>
		/// <value>
		/// The minimum length of the property.
		/// </value>
		public int MinimumPropertyLength { get; }

		/// <summary>
		/// Validates the specified property value.
		/// </summary>
		/// <param name="value">The object value.</param>
		/// <param name="propertyInfo">Information about the property containing this attribute.</param>
		/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
		public override void Validate(object value, PropertyInfo propertyInfo, IDIResolver resolver)
		{
			if (!(value is string))
				return;

			var checkValue = (string)value;

			if (checkValue.Length >= MinimumPropertyLength)
				return;

			TryThrowCustomOrStringTableException(resolver);

			throw new ModelValidationException($"Property '{propertyInfo.Name}' required minimum length is '{MinimumPropertyLength}', actual value: '{value}'");
		}
	}
}