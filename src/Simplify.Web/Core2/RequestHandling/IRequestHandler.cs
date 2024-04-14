using System;
using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.Core2.RequestHandling;

public interface IRequestHandler
{
	Task HandleAsync(IHttpContext context, Action stopProcessing);
}