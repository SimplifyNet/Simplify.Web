using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core.Controllers.Execution;

namespace Simplify.Web.RegistrationsTests.Old.CustomTypes;

public class CustomControllerExecutor : IControllerExecutor
{
	public Task<Web.Old.ControllerResponseResult> Execute(IControllerExecutionArgs args) => throw new NotImplementedException();
}