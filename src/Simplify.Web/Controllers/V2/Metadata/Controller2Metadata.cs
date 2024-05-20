using System;
using System.Collections.Generic;
using System.Reflection;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V2.Routing;

namespace Simplify.Web.Controllers.V2.Metadata;

public class Controller2Metadata : ControllerMetadata, IController2Metadata
{
	private const string InvokeMethodName = "Invoke";

	public Controller2Metadata(Type controllerType) : base(controllerType)
	{
		InvokeMethodInfo = BuildInvokeMethodInfo(InvokeMethodName);
		InvokeMethodParameters = BuildInvokeMethodParameters();
		ExecParameters = BuildControllerExecParameters(controllerType);
	}

	public MethodInfo InvokeMethodInfo { get; }
	public IDictionary<string, Type> InvokeMethodParameters { get; }

	protected override IControllerRoute BuildControllerRoute(string path) => new Controller2Route(path, InvokeMethodParameters);

	private MethodInfo BuildInvokeMethodInfo(string invokeMethodName) =>
		ControllerType.GetMethod(invokeMethodName)
		?? throw new InvalidOperationException($"Method {invokeMethodName} not found in class {ControllerType.Name}");

	private IDictionary<string, Type> BuildInvokeMethodParameters() =>
		InvokeMethodInfo
			.GetParameters()
			.ToLowercaseNameTypeDictionary();
}