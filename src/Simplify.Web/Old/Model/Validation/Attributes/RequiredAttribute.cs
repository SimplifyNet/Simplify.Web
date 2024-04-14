using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Old.Model.Validation.Attributes;

/// <summary>
/// Indicates what this property should be not null or empty.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
/// </remarks>
/// <param name="errorMessage">The custom error message, should contain string table item key if 'isMessageFromStringTable' is true.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> then indicates that errorMessage is containing string table item key instead of string error message.</param>
[AttributeUsage(AttributeTargets.Property)]
public class RequiredAttribute(string? errorMessage = null, bool isMessageFromStringTable = true) : ValidationAttribute(errorMessage, isMessageFromStringTable)
{

	/// <summary>
	/// Validates the specified property value.
	/// </summary>
	/// <param name="value">The object value.</param>
	/// <param name="propertyInfo">Information about the property containing this attribute.</param>
	/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
	/// <exception cref="ModelValidationException">
	/// Required property '{propertyInfo.Name}' is null or empty
	/// or
	/// or
	/// </exception>
	public override void Validate(object? value, PropertyInfo propertyInfo, IDIResolver resolver)
	{
		var objectIsValid = false;

		if (value != null)
		{
			objectIsValid = value switch
			{
				DateTime => !value.Equals(default(DateTime)),
				string s => !string.IsNullOrEmpty(s),
				_ => true
			};
		}

		if (objectIsValid)
			return;

		TryThrowCustomOrStringTableException(resolver);

		throw new ModelValidationException($"Required property '{propertyInfo.Name}' is null or empty");
	}
}