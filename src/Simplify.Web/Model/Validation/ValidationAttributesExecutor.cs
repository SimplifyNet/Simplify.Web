using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.DI;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Model.Validation;

/// <summary>
/// Provides the object properties validator.
/// </summary>
/// <seealso cref="IModelValidator" />
/// <remarks>
/// Initializes a new instance of the <see cref="ValidationAttributesExecutor" /> class.
/// </remarks>
/// <param name="nesting">if set to <c>true</c> then  <see cref="ValidationAttributesExecutor" /> should validate nested and inherited properties.</param>
public class ValidationAttributesExecutor(bool nesting = true) : IModelValidator
{
	/// <summary>
	/// Gets a value indicating whether <see cref="ValidationAttributesExecutor"/> should validate nested and inherited properties.
	/// </summary>
	public bool Nesting { get; } = nesting;

	/// <summary>
	/// Validates the specified model.
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	/// <param name="model">The model.</param>
	/// <param name="resolver">The resolver.</param>
	/// <exception cref="ModelValidationException"></exception>
	public void Validate<T>(T model, IDIResolver resolver) => Validate(typeof(T), model, resolver);

	/// <summary>
	/// Validates the specified value.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="propertyInfo">The property information.</param>
	/// <param name="resolver">The resolver.</param>
	/// <exception cref="ModelNotSupportedException"></exception>
	private static void ValidateProperty(object? value, PropertyInfo propertyInfo, IDIResolver resolver)
	{
		var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true).Cast<ValidationAttribute>();

		foreach (var attribute in validationAttributes)
			attribute.Validate(value, propertyInfo, resolver);
	}

	private static bool IsSystemType(Type type) => type.Namespace?.StartsWith("System") ?? false;

	private static bool IsGenericList(Type type) => type.IsGenericType && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition());

	private void Validate(Type type, object? value, IDIResolver resolver)
	{
		if (IsGenericList(type))
			return;

		if (Nesting)
			if (type.BaseType != null && type.BaseType != typeof(object) && !IsSystemType(type.BaseType))
				Validate(type.BaseType, value, resolver);

		foreach (var item in type.GetProperties())
		{
			var currentItemValue = item.GetValue(value);

			ValidateProperty(currentItemValue, item, resolver);

			if (Nesting && currentItemValue != default && !IsSystemType(item.PropertyType))
				Validate(item.PropertyType, currentItemValue, resolver);
		}
	}
}