using System.Collections.Generic;
using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing;

public class ControllerProcessingContext(
	IControllerMetaData controllerMetaData,
	SecurityStatus securityStatus,
	IHttpContext context,
	IDictionary<string, object>? routeParameters) : IControllerProcessingContext
{
	public IControllerMetaData ControllerMetaData { get; } = controllerMetaData;
	public SecurityStatus SecurityStatus { get; } = securityStatus;
	public IHttpContext Context { get; } = context;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}
