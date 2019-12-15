using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers
{
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
		public async Task<ControllersProcessorResult> ProcessControllers(IDIResolver resolver, HttpContext context)
		{
			var atleastOneNonAnyPageControllerMatched = false;

			foreach (var controller in _agent.GetStandardControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(controller, context.Request.Path.Value, context.Request.Method);

				if (matcherResult == null || !matcherResult.Success)
					continue;

				var securityResult = _agent.IsSecurityRulesViolated(controller, context.User);

				if (securityResult == SecurityRuleCheckResult.NotAuthenticated)
					return ControllersProcessorResult.Http401;

				if (securityResult == SecurityRuleCheckResult.Forbidden)
					return await ProcessForbiddenSecurityRule(resolver, context);

				var result = await ProcessController(controller, resolver, context, matcherResult.RouteParameters);

				if (result != ControllersProcessorResult.Ok)
					return result;

				if (!_agent.IsAnyPageController(controller))
					atleastOneNonAnyPageControllerMatched = true;
			}

			if (!atleastOneNonAnyPageControllerMatched)
			{
				var result = await ProcessOnlyAnyPageControllersMatched(resolver, context);

				if (result != ControllersProcessorResult.Ok)
					return result;
			}

			return ControllersProcessorResult.Ok;
		}

		private async Task<ControllersProcessorResult> ProcessController(IControllerMetaData controllerType, IDIResolver resolver, HttpContext context, dynamic routeParameters)
		{
			var result = await _controllerExecutor.Execute(controllerType, resolver, context, routeParameters);

			if (result == ControllerResponseResult.RawOutput)
				return ControllersProcessorResult.RawOutput;

			if (result == ControllerResponseResult.Redirect)
				return ControllersProcessorResult.Redirect;

			return ControllersProcessorResult.Ok;
		}

		private async Task<ControllersProcessorResult> ProcessOnlyAnyPageControllersMatched(IDIResolver resolver, HttpContext context)
		{
			var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

			if (http404Controller == null)
				return ControllersProcessorResult.Http404;

			var handlerControllerResult = await _controllerExecutor.Execute(http404Controller, resolver, context);

			if (handlerControllerResult == ControllerResponseResult.RawOutput)
				return ControllersProcessorResult.RawOutput;

			if (handlerControllerResult == ControllerResponseResult.Redirect)
				return ControllersProcessorResult.Redirect;

			return ControllersProcessorResult.Ok;
		}

		private async Task<ControllersProcessorResult> ProcessForbiddenSecurityRule(IDIResolver resolver, HttpContext context)
		{
			var http403Controller = _agent.GetHandlerController(HandlerControllerType.Http403Handler);

			if (http403Controller == null)
				return ControllersProcessorResult.Http403;

			var handlerControllerResult = await _controllerExecutor.Execute(http403Controller, resolver, context);

			if (handlerControllerResult == ControllerResponseResult.RawOutput)
				return ControllersProcessorResult.RawOutput;

			if (handlerControllerResult == ControllerResponseResult.Redirect)
				return ControllersProcessorResult.Redirect;

			return ControllersProcessorResult.Ok;
		}
	}
}