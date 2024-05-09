using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Old.Core.Controllers.Execution;
using Simplify.Web.Old.Modules;

namespace Simplify.Web.Old.Core.Controllers;

/// <summary>
/// Provides controllers processor.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllersProcessor" /> class.
/// </remarks>
/// <param name="controllersAgent">The controllers agent.</param>
/// <param name="controllerExecutor">The controller executor.</param>
/// <param name="redirector">The redirector.</param>
public class ControllersProcessor(IControllersAgent controllersAgent, IControllerExecutor controllerExecutor, IRedirector redirector) : IControllersProcessor
{
	/// <summary>
	/// Process controllers for current HTTP request
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public async Task<ControllersProcessorResult> ProcessControllers(IDIResolver resolver, HttpContext context)
	{
		var atLeastOneNonAnyPageControllerMatched = false;

		foreach (var controller in controllersAgent.GetStandardControllersMetaData())
		{
			var matcherResult = controllersAgent.MatchControllerRoute(controller, context.Request.Path.Value, context.Request.Method);

			if (matcherResult == null || !matcherResult.Success)
				continue;

			var securityResult = controllersAgent.IsSecurityRulesViolated(controller, context.User);

			if (securityResult == SecurityRuleCheckResult.NotAuthenticated)
				return ControllersProcessorResult.Http401;

			if (securityResult == SecurityRuleCheckResult.Forbidden)
				return await ProcessForbiddenSecurityRule(resolver, context);

			var result = await ProcessController(new ControllerExecutionArgs(controller, resolver, context, matcherResult.RouteParameters));

			if (result != ControllersProcessorResult.Ok)
				return result;

			if (!controllersAgent.IsAnyPageController(controller))
				atLeastOneNonAnyPageControllerMatched = true;
		}

		if (!atLeastOneNonAnyPageControllerMatched)
		{
			var result = await ProcessOnlyAnyPageControllersMatched(resolver, context);

			if (result != ControllersProcessorResult.Ok)
				return result;
		}
		else
			redirector.SetPreviousPageUrlToCurrentPage();

		return ControllersProcessorResult.Ok;
	}

	private async Task<ControllersProcessorResult> ProcessController(IControllerExecutionArgs args)
	{
		var result = await controllerExecutor.Execute(args);

		if (result == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (result == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}

	private async Task<ControllersProcessorResult> ProcessOnlyAnyPageControllersMatched(IDIResolver resolver, HttpContext context)
	{
		var http404Controller = controllersAgent.GetHandlerController(HandlerControllerType.Http404Handler);

		if (http404Controller == null)
			return ControllersProcessorResult.Http404;

		var handlerControllerResult = await controllerExecutor.Execute(new ControllerExecutionArgs(http404Controller, resolver, context));

		if (handlerControllerResult == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (handlerControllerResult == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}

	private async Task<ControllersProcessorResult> ProcessForbiddenSecurityRule(IDIResolver resolver, HttpContext context)
	{
		var http403Controller = controllersAgent.GetHandlerController(HandlerControllerType.Http403Handler);

		if (http403Controller == null)
			return ControllersProcessorResult.Http403;

		var handlerControllerResult = await controllerExecutor.Execute(new ControllerExecutionArgs(http403Controller, resolver, context));

		if (handlerControllerResult == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (handlerControllerResult == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}
}