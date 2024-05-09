using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Response;

namespace Simplify.Web.RegistrationsTests.CustomTypes;

public class CustomControllerResponseExecutor : IControllerResponseExecutor
{
	public Task<ResponseBehavior> ExecuteAsync(ControllerResponse response) => throw new NotImplementedException();
}