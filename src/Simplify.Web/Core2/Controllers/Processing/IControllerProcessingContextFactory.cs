using System.Collections.Generic;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IControllerMetaData metaData, IHttpContext context, IDictionary<string, object>? routeParameters);
}