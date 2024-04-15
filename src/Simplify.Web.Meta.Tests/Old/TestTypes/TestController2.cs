using System;
using System.Threading.Tasks;

namespace Simplify.Web.Meta.Tests.Old.TestTypes;

public class TestController2 : Web.Old.AsyncController
{
	public override Task<Web.Old.ControllerResponse?> Invoke()
	{
		throw new NotImplementedException();
	}
}