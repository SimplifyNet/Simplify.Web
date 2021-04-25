using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should match regular expression
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RegexAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegexAttribute" /> class.
		/// </summary>
		/// <param name="regexString">The regex string.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="isMessageFromStringTable">if set to <c>true</c> [is message from string table].</param>
		public RegexAttribute(string regexString, string? errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable) => RegexString = regexString;

		/// <summary>
		/// Gets or sets the regex string.
		/// </summary>
		/// <value>
		/// The regex string.
		/// </value>
		public string RegexString { get; set; }

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
}