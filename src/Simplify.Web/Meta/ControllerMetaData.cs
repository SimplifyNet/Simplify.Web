using System;

#nullable disable

namespace Simplify.Web.Meta;

/// <summary>
/// Controller meta-data information
/// </summary>
public class ControllerMetaData : IControllerMetaData
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerMetaData" /> class.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <param name="execParameters">The execute parameters.</param>
	/// <param name="role">The controller role.</param>
	/// <param name="security">The security.</param>
	public ControllerMetaData(Type controllerType, ControllerExecParameters execParameters = null, ControllerRole role = null, ControllerSecurity security = null)
	{
		ControllerType = controllerType;
		ExecParameters = execParameters;
		Role = role;
		Security = security;
	}

	/// <summary>
	/// Gets the type of the controller.
	/// </summary>
	/// <value>
	/// The type of the extension.
	/// </value>
	public Type ControllerType { get; }

	/// <summary>
	/// Gets the controller execute parameters.
	/// </summary>
	public ControllerExecParameters ExecParameters { get; }

	/// <summary>
	/// Gets the controller role information.
	/// </summary>
	public ControllerRole Role { get; }

	/// <summary>
	/// Gets the controller security information.
	/// </summary>
	/// <value>
	/// The controller security information.
	/// </value>
	public ControllerSecurity Security { get; }
}