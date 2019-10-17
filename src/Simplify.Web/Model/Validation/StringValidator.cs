using System.Reflection;
using System.Text.RegularExpressions;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Model.Validation
{
	/// <summary>
	/// Validates string using specified rules in attributes
	/// </summary>
	public class StringValidator
	{
		/// <summary>
		/// Validates the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <exception cref="ModelValidationException">
		/// </exception>
		public static void Validate(string value, PropertyInfo propertyInfo)
		{
			var attributes = propertyInfo.GetCustomAttributes(typeof(RegexAttribute), false);

			if (attributes.Length > 0)
			{
				var regexString = ((RegexAttribute)attributes[0]).RegexString;

				if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, regexString))
					throw new ModelValidationException(
						$"Property '{propertyInfo.Name}' regex not matched, actual value: '{value}', pattern: '{regexString}'");
			}
		}
	}
}