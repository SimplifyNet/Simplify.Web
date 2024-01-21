using System;
using System.Threading.Tasks;
using Simplify.Web.Core.Controllers.Execution;

namespace Simplify.Web.RegistrationsTests.CustomTypes;

public class CustomControllerExecutor : IControllerExecutor
{
	public Task<ControllerResponseResult> Execute(IControllerExecutionArgs args) => throw new NotImplementedException();
}