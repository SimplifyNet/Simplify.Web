using System;
using System.Linq;
using System.Reflection;
using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.ModelBinding.Validation;

/// <summary>
/// Provides object properties validator
/// </summary>
public class ObjectPropertiesValidator : IModelValidator
{
	private static readonly Type RequiredAttributeType = typeof(RequiredAttribute);

	/// <summary>
	/// Validates the specified model.
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	/// <param name="model">The model.</param>
	/// <exception cref="ModelValidationException"></exception>
	public void Validate<T>(T model)
	{
		var type = typeof(T);

		foreach (var propInfo in type.GetProperties())
		{
			var isRequired = propInfo.CustomAttributes.Any(x => x.AttributeType == RequiredAttributeType);

			if (isRequired && !Validate(propInfo.GetValue(model), propInfo))
				throw new ModelValidationException($"Required property '{propInfo.Name}' is null or empty");
		}
	}

	/// <summary>
	/// Validates the specified value.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="propertyInfo">The property information.</param>
	/// <returns></returns>
	/// <exception cref="ModelNotSupportedException"></exception>
	private static bool Validate(object value, PropertyInfo propertyInfo)
	{
		if (propertyInfo.PropertyType == typeof(string))
		{
			StringValidator.Validate((string)value, propertyInfo);

			return !string.IsNullOrEmpty((string)value);
		}

		if (propertyInfo.PropertyType == typeof(DateTime))
			return (((DateTime)value) != default(DateTime));

		if (propertyInfo.PropertyType == typeof(DateTime?))
			return (((DateTime?)value) != null);

		return value != null;
	}
}