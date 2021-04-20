using System;
using System.Threading.Tasks;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestController5 : AsyncController<TestModel>
	{
		public override Task<ControllerResponse> Invoke() => throw new NotImplementedException();
	}
}