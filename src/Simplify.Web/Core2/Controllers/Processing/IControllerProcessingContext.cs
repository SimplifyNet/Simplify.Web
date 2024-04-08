using System.Collections.Generic;
using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingContext
{
	public IControllerMetaData ControllerMetaData { get; set; }

	public SecurityStatus SecurityStatus { get; set; }

	public IHttpContext Context { get; set; }

	public IDictionary<string, object>? RouteParameters { get; }
}