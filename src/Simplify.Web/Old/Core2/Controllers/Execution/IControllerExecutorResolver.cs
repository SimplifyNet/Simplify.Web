using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Execution;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetaData controller);
}
