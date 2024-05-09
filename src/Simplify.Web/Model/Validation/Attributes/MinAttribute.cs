using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets the minimum required property value
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MinAttribute"/> class.
/// </remarks>
/// <param name="minValue">Minimum value of the property.</param>
/// <param name="errorMessage">The error message.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
[AttributeUsage(AttributeTargets.Property)]
public class MinAttribute(IComparable minValue, string? errorMessage = null, bool isMessageFromStringTable = true)
	: ValidationAttribute(errorMessage, isMessageFromStringTable)
{
	/// <summary>
	/// Gets the minimum value of the property.
	/// </summary>
	public IComparable MinValue { get; } = minValue;

	/// <summary>
	/// Validates the specified property value.
	/// </summary>
	/// <param name="value">The object value.</param>
	/// <param name="propertyInfo">Information about the property containing this attribute.</param>
	/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
	public override void Validate(object? value, PropertyInfo propertyInfo, IDIResolver resolver)
	{
		if (value == null)
			return;

		if (value is not IComparable comparableValue)
			throw new ArgumentException($"The type of specified property value must be inherited from {typeof(IComparable)}");

		ValidateTypesMatching(comparableValue);

		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(MinValue) < 0)
			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required minimum value is {MinValue}, actual value: {value}");
	}

	private void ValidateTypesMatching(IComparable comparableValue)
	{
		if (comparableValue.GetType() != MinValue.GetType())
			throw new ArgumentException("Type mismatch. The minimum value and property value should be of the same type.");
	}
}