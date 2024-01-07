using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets a range constraint
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RangeAttribute : ValidationAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RangeAttribute"/> class.
	/// </summary>
	/// <param name="minValue">The minimum value, inclusive.</param>
	/// <param name="maxValue">The maximum value, inclusive.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public RangeAttribute(IComparable minValue, IComparable maxValue,  string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable)
	{
		MinValue = minValue;
		MaxValue = maxValue;
	}
	
	/// <summary>
	/// Gets or sets the minimum value for the range.
	/// </summary>
	/// <value>
	/// The minimum value for the range.
	/// </value>
	public IComparable MinValue { get; }

	/// <summary>
	/// Gets or sets the maximum value for the range.
	/// </summary>
	/// <value>
	/// The maximum value for the range.
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
			throw new ArgumentException($"The type of specified property value must be inherited from {typeof(IComparable)}");

		ValidateTypesMatching(comparableValue);
		
		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(MinValue) < 0 || comparableValue.CompareTo(MaxValue) > 0)
			throw new ModelValidationException(
				$"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}");
	}
	
	private void ValidateTypesMatching(IComparable comparableValue)
	{
		if (comparableValue.GetType() != MinValue.GetType())
			throw new ArgumentException("Type mismatch. The minimum value and property value should be of the same type.");
		
		if (comparableValue.GetType() != MaxValue.GetType())
			throw new ArgumentException("Type mismatch. The maximum value and property value should be of the same type.");
	}
}