using System.Collections.Generic;
using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingContext
{
	public IControllerMetaData ControllerMetaData { get; }

	public SecurityStatus SecurityStatus { get; }

	public IHttpContext Context { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}