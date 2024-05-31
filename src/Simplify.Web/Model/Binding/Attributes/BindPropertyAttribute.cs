using System;

namespace Simplify.Web.Model.Binding.Attributes;

/// <summary>
/// Sets the source field name this property binds to.
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="BindPropertyAttribute" /> class.
/// </remarks>
/// <param name="fieldName">Name of the field.</param>
[AttributeUsage(AttributeTargets.Property)]
public class BindPropertyAttribute(string fieldName) : Attribute
{
	/// <summary>
	/// Gets the name of the field.
	/// </summary>
	public string FieldName { get; } = fieldName;
}