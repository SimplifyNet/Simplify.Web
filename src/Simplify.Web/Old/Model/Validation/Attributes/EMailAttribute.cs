using System;
using System.Reflection;
using Simplify.DI;
using Simplify.String;

namespace Simplify.Web.Old.Model.Validation.Attributes;

/// <summary>
/// Indicates that this property should be a valid email address.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EMailAttribute"/> class.
/// </remarks>
/// <param name="errorMessage">The custom error message, should contain string table item key if 'isMessageFromStringTable' is true.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> then indicates that errorMessage is containing string table item key instead of string error message.</param>
[AttributeUsage(AttributeTargets.Property)]
public class EMailAttribute(string? errorMessage = null, bool isMessageFromStringTable = true) : ValidationAttribute(errorMessage, isMessageFromStringTable)
{

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

		if (StringHelper.ValidateEMail(s))
			return;

		TryThrowCustomOrStringTableException(resolver);

		throw new ModelValidationException($"Property '{propertyInfo.Name}' should be an email, actual value: '{s}'");
	}
}