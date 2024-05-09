using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Indicates that this property should match regular expression.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RegexAttribute" /> class.
/// </remarks>
/// <param name="regexString">The regex string.</param>
/// <param name="errorMessage">The error message.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
[AttributeUsage(AttributeTargets.Property)]
public class RegexAttribute(string regexString, string? errorMessage = null, bool isMessageFromStringTable = true) : ValidationAttribute(errorMessage, isMessageFromStringTable)
{
	/// <summary>
	/// Gets the regex string.
	/// </summary>
	public string RegexString { get; } = regexString;

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

		if (Regex.IsMatch(s, RegexString))
			return;

		TryThrowCustomOrStringTableException(resolver);

		throw new ModelValidationException($"Property '{propertyInfo.Name}' regex not matched, actual value: '{s}', pattern: '{RegexString}'");
	}
}