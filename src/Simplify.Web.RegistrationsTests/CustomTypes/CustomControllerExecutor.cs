using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Meta;

namespace Simplify.Web.RegistrationsTests.CustomTypes;

public class CustomControllerExecutor : IControllerExecutor
{
	public Task<ControllerResponseResult> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context, IDictionary<string, object>? routeParameters = null) =>
		throw new NotImplementedException();
}