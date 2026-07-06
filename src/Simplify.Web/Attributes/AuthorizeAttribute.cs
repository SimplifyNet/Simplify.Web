using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates whether the controller requires user authorization.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Class)]
public class AuthorizeAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
	/// </summary>
	/// <param name="requiredUserRoles">Required user roles.</param>
	public AuthorizeAttribute(string? requiredUserRoles = null) =>
		RequiredUserRoles = !string.IsNullOrEmpty(requiredUserRoles)
			? requiredUserRoles!.ParseCommaSeparatedList()
			: [];

	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeAttribute" /> class.
	/// </summary>
	/// <param name="requiredUserRoles">The required user roles.</param>
	public AuthorizeAttribute(params string[] requiredUserRoles) =>
		RequiredUserRoles = requiredUserRoles.Where(r => !string.IsNullOrEmpty(r));

	/// <summary>
	/// Gets the required user roles.
	/// </summary>
	public IEnumerable<string> RequiredUserRoles { get; }
}