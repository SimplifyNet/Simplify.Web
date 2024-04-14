using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Old.Model.Validation.Attributes;

/// <summary>
/// Sets minimum required property length.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MinLengthAttribute" /> class.
/// </remarks>
/// <param name="minimumPropertyLength">Minimum length of the property.</param>
/// <param name="errorMessage">The error message.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
[AttributeUsage(AttributeTargets.Property)]
public class MinLengthAttribute(int minimumPropertyLength,
	string? errorMessage = null,
	bool isMessageFromStringTable = true) : ValidationAttribute(errorMessage, isMessageFromStringTable)
{

	/// <summary>
	/// Gets or sets the minimum length of the property.
	/// </summary>
	/// <value>
	/// The minimum length of the property.
	/// </value>
	public int MinimumPropertyLength { get; } = minimumPropertyLength;

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

		if (s.Length >= MinimumPropertyLength)
			return;

		TryThrowCustomOrStringTableException(resolver);

		throw new ModelValidationException($"Property '{propertyInfo.Name}' required minimum length is '{MinimumPropertyLength}', actual value: '{s}'");
	}
}