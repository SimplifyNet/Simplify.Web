using System;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionStage
{
	void Execute(ControllerResolutionState state, HttpContext context, Action stopExecution);
}