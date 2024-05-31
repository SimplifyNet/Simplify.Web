using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V2.Routing;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V2.Metadata;

/// <summary>
/// Provides the controller v2 metadata.
/// </summary>
/// <seealso cref="ControllerMetadata" />
/// <seealso cref="IController2Metadata" />
public class Controller2Metadata : ControllerMetadata, IController2Metadata
{
	private const string InvokeMethodName = "Invoke";

	/// <summary>
	/// Initializes a new instance of the <see cref="Controller2Metadata"/> class.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ControllerMetadata" /> class.
	/// </remarks>
	/// <seealso cref="IControllerMetadata" />
	public Controller2Metadata(Type controllerType) : base(controllerType)
	{
		InvokeMethodInfo = BuildInvokeMethodInfo(InvokeMethodName);
		InvokeMethodParameters = BuildInvokeMethodParameters();
		ExecParameters = BuildControllerExecParameters(controllerType);

		ValidateReturnType(InvokeMethodInfo);
	}

	/// <summary>
	/// Gets the invoke method information.
	/// </summary>
	/// <value>
	/// The invoke method information.
	/// </value>
	public MethodInfo InvokeMethodInfo { get; }

	/// <summary>
	/// Gets the invoke method parameters.
	/// </summary>
	/// <value>
	/// The invoke method parameters.
	/// </value>
	public IDictionary<string, Type> InvokeMethodParameters { get; }

	/// <summary>
	/// Builds the controller route.
	/// </summary>
	/// <param name="path">The path.</param>
	protected override IControllerRoute BuildControllerRoute(string path) => new Controller2Route(path, InvokeMethodParameters);

	private static void ValidateReturnType(MethodInfo methodInfo)
	{
		if (!Controller2ValidReturnTypes.Types.Contains(methodInfo.ReturnType))
			throw new InvalidOperationException($"Invoke method invalid return type, can be one of: {Controller2ValidReturnTypes.Types.GetTypeNamesAsString()}");
	}

	private MethodInfo BuildInvokeMethodInfo(string invokeMethodName) =>
			ControllerType.GetMethod(invokeMethodName)
		?? throw new InvalidOperationException($"Method {invokeMethodName} not found in class {ControllerType.Name}");

	private IDictionary<string, Type> BuildInvokeMethodParameters() =>
		InvokeMethodInfo
			.GetParameters()
			.ToLowercaseNameTypeDictionary();
}