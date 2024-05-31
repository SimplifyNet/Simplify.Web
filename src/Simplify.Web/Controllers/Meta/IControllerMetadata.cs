using System;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Represents a controller base metadata information.
/// </summary>
public interface IControllerMetadata
{
	/// <summary>
	/// Gets the type of the controller.
	/// </summary>
	/// <value>
	/// The type of the controller.
	/// </value>
	Type ControllerType { get; }

	/// <summary>
	/// Gets the controller execute parameters.
	/// </summary>
	/// <value>
	/// The execute parameters.
	/// </value>
	ControllerExecParameters? ExecParameters { get; }

	/// <summary>
	/// Gets the controller role information.
	/// </summary>
	/// <value>
	/// The role.
	/// </value>
	ControllerRole? Role { get; }

	/// <summary>
	/// Gets the controller security information.
	/// </summary>
	/// <value>
	/// The security.
	/// </value>
	ControllerSecurity? Security { get; }
}