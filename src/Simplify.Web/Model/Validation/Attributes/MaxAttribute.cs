using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets maximum required property value
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MaxAttribute : ValidationAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MaxAttribute"/> class.
	/// </summary>
	/// <param name="maxValue">Maximum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MaxAttribute(IComparable maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable) => MaxValue = maxValue;

	/// <summary>
	/// Gets or sets the maximum value of the property.
	/// </summary>
	/// <value>
	/// The maximum value of the property.
	/// </value>
	public IComparable MaxValue { get; }

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
			throw new ModelValidationException($"The type of specified property value must be inherited from {typeof(IComparable)}");

		ValidateTypesMatching(comparableValue);
		
		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(MaxValue) > 0)
			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required maximum value is {MaxValue}, actual value: {value}");
	}
	
	private void ValidateTypesMatching(IComparable comparableValue)
	{
		if (comparableValue.GetType() != MaxValue.GetType())
			throw new ModelValidationException("Type mismatch. The maximum value and property value should be of the same type.");
	}
}