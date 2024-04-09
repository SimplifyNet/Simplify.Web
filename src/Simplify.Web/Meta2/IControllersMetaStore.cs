using System.Collections.Generic;

namespace Simplify.Web.Meta2;

/// <summary>
/// Represent controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	/// <summary>
	/// Current domain controllers metadata
	/// </summary>
	/// <returns></returns>
	IList<IControllerMetaData> ControllersMetaData { get; }
}