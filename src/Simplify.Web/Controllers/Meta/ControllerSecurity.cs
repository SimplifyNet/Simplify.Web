using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller security information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerSecurity" /> class.
/// </remarks>
/// <param name="isAuthorizationRequired">if set to <c>true</c> then indicates whether controller requires user authorization.</param>
/// <param name="requiredUserRoles">The required user roles.</param>
public class ControllerSecurity(bool isAuthorizationRequired = false, IEnumerable<string>? requiredUserRoles = null)
{
	/// <summary>
	/// Gets a value indicating whether controller requires user authorization.
	/// </summary>
	public bool IsAuthorizationRequired { get; } = isAuthorizationRequired;

	/// <summary>
	/// Gets the required user roles.
	/// </summary>
	public IEnumerable<string>? RequiredUserRoles { get; } = requiredUserRoles;
}