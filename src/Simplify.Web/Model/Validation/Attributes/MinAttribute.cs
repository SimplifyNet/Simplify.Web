using System;
using System.Globalization;
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
	public MinAttribute(int minValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		OperandType = typeof(int);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MinAttribute"/> class.
	/// </summary>
	/// <param name="minValue">Minimum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MinAttribute(long minValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		OperandType = typeof(long);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MinAttribute"/> class.
	/// </summary>
	/// <param name="minValue">Minimum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MinAttribute(double minValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		OperandType = typeof(double);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MinAttribute"/> class.
	/// </summary>
	/// <param name="type">The type of the minimum value of the property.</param>
	/// <param name="minValue">Minimum value of the property.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
	public MinAttribute(Type type, string minValue, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage,
		isMessageFromStringTable)
	{
		MinValue = minValue;
		OperandType = type;
	}

	/// <summary>
	/// Gets or sets the minimum value of the property.
	/// </summary>
	/// <value>
	/// The minimum value of the property.
	/// </value>
	public object MinValue { get; }

	/// <summary>
	/// Gets the type of the minimum value.
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

		var minValue = ConvertToIComparable(MinValue);
		var comparableValue = (IComparable)value;

		ValidateTypesMatching(value);

		TryThrowCustomOrStringTableException(resolver);

		if (comparableValue.CompareTo(minValue) < 0)
			throw new ModelValidationException(
				$"Property '{propertyInfo.Name}' required minimum value is {MinValue}, actual value: {value}");
	}

	private void ValidateTypesMatching(object value)
	{
		if (value.GetType() != OperandType)
			throw new ArgumentException("Type mismatch. The minimum value and property value should be of the same type.");
	}

	private IComparable ConvertToIComparable(object value)
	{
		var convertedValue = Convert.ChangeType(value!, OperandType, CultureInfo.InvariantCulture);

		if (convertedValue is not IComparable comparableValue)
			throw new ArgumentException($"The type of specified property value must be inherited from {typeof(IComparable)}");

		return comparableValue;
	}
}