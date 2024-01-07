using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets minimum required property value
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MinAttribute : ValidationAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MinAttribute"/> class.
	/// </summary>
	/// <param name="minValue">Minimum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MinAttribute(IComparable minValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable) => MinValue = minValue;

	/// <summary>
	/// Gets or sets the minimum value of the property.
	/// </summary>
	/// <value>
	/// The minimum value of the property.
	/// </value>
	public IComparable MinValue { get; }

	/// <summary>
	/// Validates the specified property value.
	/// </summary>
	/// <param name="value">The object value.</param>
	/// <param name="propertyInfo">Information about the property containing this attribute.</param>
	/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
	public override void Validate(object? value, PropertyInfo propertyInfo, IDIResolver resolver)
	{
		if (value is not IComparable comparableValue || comparableValue.GetType() != MinValue.GetType())
			return;
		
		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(MinValue) < 0)
			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required minimum value is {MinValue}, actual value: {value}");
	}
}