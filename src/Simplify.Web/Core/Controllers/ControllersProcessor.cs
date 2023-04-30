using System;
using Microsoft.Owin;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;

namespace Simplify.Web.Core.Controllers;

/// <summary>
/// Provides controllers processor
/// </summary>
public class ControllersProcessor : IControllersProcessor
{
	private readonly IControllersAgent _agent;
	private readonly IControllerExecutor _controllerExecutor;

	/// <summary>
	/// Initializes a new instance of the <see cref="ControllersProcessor" /> class.
	/// </summary>
	/// <param name="controllersAgent">The controllers agent.</param>
	/// <param name="controllerExecutor">The controller executor.</param>
	public ControllersProcessor(IControllersAgent controllersAgent, IControllerExecutor controllerExecutor)
	{
		_agent = controllersAgent;
		_controllerExecutor = controllerExecutor;
	}

	/// <summary>
	/// Process controllers for current HTTP request
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public ControllersProcessorResult ProcessControllers(IDIResolver resolver, IOwinContext context)
	{
		var atleastOneNonAnyPageControllerMatched = false;

		foreach (var metaData in _agent.GetStandardControllersMetaData())
		{
			var matcherResult = _agent.MatchControllerRoute(metaData, context.Request.Path.Value, context.Request.Method);

			if (matcherResult == null || !matcherResult.Success) continue;

			var securityResult = _agent.IsSecurityRulesViolated(metaData, context.Authentication.User);

			if (securityResult == SecurityRuleCheckResult.NotAuthenticated)
				return ControllersProcessorResult.Http401;

			if (securityResult == SecurityRuleCheckResult.Forbidden)
				return ProcessForbiddenSecurityRule(resolver, context);

			var result = ProcessController(metaData.ControllerType, resolver, context, matcherResult.RouteParameters);

			if (result != ControllersProcessorResult.Ok)
				return result;

			if (!_agent.IsAnyPageController(metaData))
				atleastOneNonAnyPageControllerMatched = true;
		}

		if (!atleastOneNonAnyPageControllerMatched)
		{
			var result = ProcessOnlyAnyPageControllersMatched(resolver, context);
			if (result != ControllersProcessorResult.Ok)
				return result;
		}

		return ProcessAsyncControllersResponses(resolver);
	}

	private ControllersProcessorResult ProcessController(Type controllerType, IDIResolver resolver, IOwinContext context, dynamic routeParameters)
	{
		var result = _controllerExecutor.Execute(controllerType, resolver, context, routeParameters);

		if (result == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (result == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}

	private ControllersProcessorResult ProcessOnlyAnyPageControllersMatched(IDIResolver resolver, IOwinContext context)
	{
		var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

		if (http404Controller == null)
			return ControllersProcessorResult.Http404;

		var handlerControllerResult = _controllerExecutor.Execute(http404Controller.ControllerType, resolver, context);

		if (handlerControllerResult == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (handlerControllerResult == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}

	private ControllersProcessorResult ProcessAsyncControllersResponses(IDIResolver resolver)
	{
		foreach (var controllerResponseResult in _controllerExecutor.ProcessAsyncControllersResponses(resolver))
		{
			if (controllerResponseResult == ControllerResponseResult.RawOutput)
				return ControllersProcessorResult.RawOutput;

			if (controllerResponseResult == ControllerResponseResult.Redirect)
				return ControllersProcessorResult.Redirect;
		}

		return ControllersProcessorResult.Ok;
	}

	private ControllersProcessorResult ProcessForbiddenSecurityRule(IDIResolver resolver, IOwinContext context)
	{
		var http403Controller = _agent.GetHandlerController(HandlerControllerType.Http403Handler);

		if (http403Controller == null)
			return ControllersProcessorResult.Http403;

		var handlerControllerResult = _controllerExecutor.Execute(http403Controller.ControllerType, resolver, context);

		if (handlerControllerResult == ControllerResponseResult.RawOutput)
			return ControllersProcessorResult.RawOutput;

		if (handlerControllerResult == ControllerResponseResult.Redirect)
			return ControllersProcessorResult.Redirect;

		return ControllersProcessorResult.Ok;
	}
}