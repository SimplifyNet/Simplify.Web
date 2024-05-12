using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionStage
{
	void Execute(IControllerResolutionState state, HttpContext context, Action stopExecution);
}