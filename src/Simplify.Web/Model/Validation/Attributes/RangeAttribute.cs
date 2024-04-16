using System;
using System.Globalization;
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
	public RangeAttribute(int minValue, int maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable)
	{
		MinValue = minValue;
		MaxValue = maxValue;
		OperandType = typeof(int);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="RangeAttribute"/> class.
	/// </summary>
	/// <param name="minValue">The minimum value, inclusive.</param>
	/// <param name="maxValue">The maximum value, inclusive.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public RangeAttribute(long minValue, long maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		MaxValue = maxValue;
		OperandType = typeof(long);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="RangeAttribute"/> class.
	/// </summary>
	/// <param name="minValue">The minimum value, inclusive.</param>
	/// <param name="maxValue">The maximum value, inclusive.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public RangeAttribute(double minValue, double maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		MaxValue = maxValue;
		OperandType = typeof(double);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="RangeAttribute"/> class.
	/// </summary>
	/// <param name="type">The type of the maximum and minimum values of the property.</param>
	/// <param name="minValue">The minimum value, inclusive.</param>
	/// <param name="maxValue">The maximum value, inclusive.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public RangeAttribute(Type type, string minValue, string maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		MaxValue = maxValue;
		OperandType = type;
	}

	/// <summary>
	/// Gets or sets the minimum value for the range.
	/// </summary>
	/// <value>
	/// The minimum value for the range.
	/// </value>
	public object MinValue { get; }

	/// <summary>
	/// Gets or sets the maximum value for the range.
	/// </summary>
	/// <value>
	/// The maximum value for the range.
	/// </value>
	public object MaxValue { get; }

	/// <summary>
	/// Gets the type of the maximum and minimum values.
	/// </summary>
	public Type OperandType { get; }

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

		var minValue = ConvertToOperandComparableType(MinValue);
		var maxValue = ConvertToOperandComparableType(MaxValue);

		var comparableValue = ConvertToIComparable(value);

		ValidateTypesMatching(comparableValue);

		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(minValue) < 0 || comparableValue.CompareTo(maxValue) > 0)
			throw new ModelValidationException(
				$"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}");
	}

	private void ValidateTypesMatching(IComparable comparableValue)
	{
		if (comparableValue.GetType() != OperandType)
			throw new ArgumentException("Type mismatch. The minimum value and property value should be of the same type.");

		if (comparableValue.GetType() != OperandType)
			throw new ArgumentException("Type mismatch. The maximum value and property value should be of the same type.");
	}

	private IComparable ConvertToOperandComparableType(object value)
	{
		var convertedValue = Convert.ChangeType(value!, OperandType, CultureInfo.InvariantCulture);

		return ConvertToIComparable(convertedValue);
	}

	private IComparable ConvertToIComparable(object value)
	{
		if (value is not IComparable comparableValue)
			throw new ArgumentException($"The type of object value must be inherited from {typeof(IComparable)}");

		return comparableValue;
	}
}