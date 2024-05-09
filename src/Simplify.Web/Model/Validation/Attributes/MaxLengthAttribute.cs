using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets the maximum required property length.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MaxLengthAttribute" /> class.
/// </remarks>
/// <param name="maximumPropertyLength">Maximum length of the property.</param>
/// <param name="errorMessage">The error message.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
[AttributeUsage(AttributeTargets.Property)]
public class MaxLengthAttribute(int maximumPropertyLength,
	string? errorMessage = null,
	bool isMessageFromStringTable = true) : ValidationAttribute(errorMessage, isMessageFromStringTable)
{
	/// <summary>
	/// Gets the maximum length of the property.
	/// </summary>
	public int MaximumPropertyLength { get; set; } = maximumPropertyLength;

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