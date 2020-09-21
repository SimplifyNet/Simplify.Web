using System;
using System.Linq;
using System.Reflection;
using Simplify.DI;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Model.Validation
{
	/// <summary>
	/// Provides object properties validator
	/// </summary>
	public class ValidationAttributesExecutor : IModelValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationAttributesExecutor"/> class.
		/// </summary>
		/// <param name="nesting">if set to <c>true</c> then  <see cref="ValidationAttributesExecutor"/> should validate nested and inherited properties.</param>
		public ValidationAttributesExecutor(bool nesting = true)
		{
			Nesting = nesting;
		}

		/// <summary>
		/// Gets a value indicating whether <see cref="ValidationAttributesExecutor"/> should validate nested and inherited properties.
		/// </summary>
		public bool Nesting { get; }

		/// <summary>
		/// Validates the specified model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="resolver">The resolver.</param>
		/// <exception cref="ModelValidationException"></exception>
		public void Validate<T>(T model, IDIResolver resolver)
		{
			Validate(typeof(T), model, resolver);
		}

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

		private void Validate(Type type, object? value, IDIResolver resolver)
		{
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

		private bool IsSystemType(Type type)
		{
			return type.Namespace?.StartsWith("System") ?? false;
		}
	}
}