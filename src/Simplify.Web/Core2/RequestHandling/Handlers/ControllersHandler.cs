using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class ControllersHandler(IControllersRequestHandler requestHandler) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing)
	{
		await requestHandler.HandleAsync(null!);
	}
}