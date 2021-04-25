using System;

namespace Simplify.Web.Model.Binding.Attributes
{
	/// <summary>
	/// Sets source field name this property binds to
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class BindPropertyAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BindPropertyAttribute"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		public BindPropertyAttribute(string fieldName) => FieldName = fieldName;

		/// <summary>
		/// Gets or sets the name of the field.
		/// </summary>
		/// <value>
		/// The name of the field.
		/// </value>
		public string FieldName { get; set; }
	}
}