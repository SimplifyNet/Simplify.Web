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
	Type ControllerType { get; }

	/// <summary>
	/// Gets the controller execute parameters.
	/// </summary>
	ControllerExecParameters? ExecParameters { get; }

	/// <summary>
	/// Gets the controller role information.
	/// </summary>
	ControllerRole? Role { get; }

	/// <summary>
	/// Gets the controller security information.
	/// </summary>
	ControllerSecurity? Security { get; }
}