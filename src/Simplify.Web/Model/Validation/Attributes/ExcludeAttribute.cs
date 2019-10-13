using System;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should be excluded from model binding
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExcludeAttribute : Attribute
	{
	}
}