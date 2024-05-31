using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security;

/// <summary>
/// Represents a security rule
/// </summary>
public interface ISecurityRule
{
	/// <summary>
	/// Gets the violation status.
	/// </summary>
	/// <value>
	/// The violation status.
	/// </value>
	SecurityStatus ViolationStatus { get; }

	/// <summary>
	/// Determines whether this security rule is violated.
	/// </summary>
	/// <param name="security">The security.</param>
	/// <param name="user">The user.</param>
	/// <returns>
	///   <c>true</c> if this security rule is violated; otherwise, <c>false</c>.
	/// </returns>
	bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user);
}