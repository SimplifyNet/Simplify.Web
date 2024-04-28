using System;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionStage
{
	void Execute(ControllerResolutionState state, IHttpContext context, Action stopExecution);
}