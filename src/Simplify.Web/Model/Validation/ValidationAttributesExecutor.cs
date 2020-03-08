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
		/// Validates the specified model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="resolver">The resolver.</param>
		/// <exception cref="ModelValidationException"></exception>
		public void Validate<T>(T model, IDIResolver resolver)
		{
			var type = typeof(T);

			foreach (var item in type.GetProperties())
				ValidateProperty(item.GetValue(model), item, resolver);
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
	}
}