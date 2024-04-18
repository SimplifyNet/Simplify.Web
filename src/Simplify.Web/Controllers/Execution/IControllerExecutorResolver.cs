using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetadata controller);
}