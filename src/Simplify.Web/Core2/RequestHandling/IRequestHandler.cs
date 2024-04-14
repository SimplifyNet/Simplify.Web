using System;
using System.Threading.Tasks;

namespace Simplify.Web.Core2.RequestHandling;

public interface IRequestHandler
{
	Task HandleAsync(IHttpContext context, Action stopProcessing);
}