using System;

namespace Simplify.Web.Old.Model.Binding.Attributes;

/// <summary>
/// Sets source field name this property binds to.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BindPropertyAttribute"/> class.
/// </remarks>
/// <param name="fieldName">Name of the field.</param>
[AttributeUsage(AttributeTargets.Property)]
public class BindPropertyAttribute(string fieldName) : Attribute
{

	/// <summary>
	/// Gets or sets the name of the field.
	/// </summary>
	/// <value>
	/// The name of the field.
	/// </value>
	public string FieldName { get; set; } = fieldName;
}