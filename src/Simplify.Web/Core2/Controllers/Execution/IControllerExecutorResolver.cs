using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Execution;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetaData controller);
}
