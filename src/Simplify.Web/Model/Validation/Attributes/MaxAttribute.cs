using System;
using System.Globalization;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Sets the maximum required property value
/// </summary>
/// <seealso cref="ValidationAttribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="MaxAttribute" /> class.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class MaxAttribute : ValidationAttribute
{
	/// <summary>
	/// Gets the maximum value of the property.
	/// </summary>
	/// <param name="maxValue">Maximum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MaxAttribute(int maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MaxValue = maxValue;
		OperandType = typeof(int);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MaxAttribute"/> class.
	/// </summary>
	/// <param name="maxValue">Maximum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MaxAttribute(long maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MaxValue = maxValue;
		OperandType = typeof(long);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MaxAttribute"/> class.
	/// </summary>
	/// <param name="maxValue">Maximum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MaxAttribute(double maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MaxValue = maxValue;
		OperandType = typeof(double);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MaxAttribute"/> class.
	/// </summary>
	/// <param name="type">The type of the maximum value of the property.</param>
	/// <param name="maxValue">Maximum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MaxAttribute(Type type, string maxValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MaxValue = maxValue;
		OperandType = type;
	}

	/// <summary>
	/// Gets or sets the maximum value of the property.
	/// </summary>
	/// <value>
	/// The maximum value of the property.
	/// </value>
	public object MaxValue { get; }

	/// <summary>
	/// Gets the type of the maximum value.
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

		var maxValue = ConvertToOperandComparableType(MaxValue);

		var comparableValue = ConvertToIComparable(value);

		ValidateTypesMatching(value);

		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(maxValue) > 0)
			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required maximum value is {MaxValue}, actual value: {value}");
	}

	private static IComparable ConvertToIComparable(object value)
	{
		if (value is not IComparable comparableValue)
			throw new ArgumentException($"The type of object value must be inherited from {typeof(IComparable)}");

		return comparableValue;
	}

	private void ValidateTypesMatching(object value)
	{
		if (value.GetType() != OperandType)
			throw new ArgumentException("Type mismatch. The maximum value and property value should be of the same type.");
	}

	private IComparable ConvertToOperandComparableType(object value)
	{
		var convertedValue = Convert.ChangeType(value!, OperandType, CultureInfo.InvariantCulture);

		return ConvertToIComparable(convertedValue);
	}
}