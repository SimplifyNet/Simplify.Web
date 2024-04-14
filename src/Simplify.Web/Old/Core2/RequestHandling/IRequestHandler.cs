using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.RequestHandling;

public interface IRequestHandler
{
	Task HandleAsync(IHttpContext context, Action stopProcessing);
}