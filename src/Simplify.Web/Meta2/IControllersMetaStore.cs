using System.Collections.Generic;

namespace Simplify.Web.Meta2;

/// <summary>
/// Represent controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	IList<IControllerMetaData> StandardControllers { get; }

	IControllerMetaData Controller400 { get; }
	IControllerMetaData Controller403 { get; }
	IControllerMetaData Controller404 { get; }
}