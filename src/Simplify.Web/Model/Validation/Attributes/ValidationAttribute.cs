using System;
using System.Reflection;
using Simplify.DI;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Model.Validation.Attributes;

/// <summary>
/// Provides the model property validation attribute base class
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="ValidationAttribute" /> class.
/// </remarks>
/// <param name="errorMessage">The custom error message, should contain string table item key if 'isMessageFromStringTable' is true.</param>
/// <param name="isMessageFromStringTable">if set to <c>true</c> then indicates that errorMessage is containing string table item key instead of string error message.</param>
[AttributeUsage(AttributeTargets.Property)]
public abstract class ValidationAttribute(string? errorMessage, bool isMessageFromStringTable) : Attribute
{
	/// <summary>
	/// Gets the error message.
	/// </summary>
	protected string? ErrorMessage { get; } = errorMessage;

	/// <summary>
	/// Gets a value indicating whether the ErrorMessage is a message from string table.
	/// </summary>
	protected bool IsMessageFromStringTable { get; } = isMessageFromStringTable;

	/// <summary>
	/// Validates the specified property value.
	/// </summary>
	/// <param name="value">The object value.</param>
	/// <param name="propertyInfo">Information about the property containing this attribute.</param>
	/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
	public abstract void Validate(object? value, PropertyInfo propertyInfo, IDIResolver resolver);

	/// <summary>
	/// Throws the custom or string table exception.
	/// </summary>
	/// <param name="resolver">The resolver.</param>
	/// <exception cref="ModelValidationException">
	/// </exception>
	protected void TryThrowCustomOrStringTableException(IDIResolver resolver)
	{
		if (string.IsNullOrEmpty(ErrorMessage))
			return;

		if (IsMessageFromStringTable)
			throw new ModelValidationException(resolver.Resolve<IStringTable>().GetItem(ErrorMessage!) ?? "");

		throw new ModelValidationException(ErrorMessage!);
	}
}