#nullable disable

using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Model;

namespace Simplify.Web;

/// <summary>
/// Model controllers base class version 2
/// </summary>
public abstract class Controller2<T> : ResponseShortcutsControllerBase
	where T : class
{
	private T _model;

	/// <summary>
	/// Gets the model (and reads it synchronously, if it is null) of a current request.
	/// </summary>
	/// <value>
	/// The current request model.
	/// </value>
	public virtual T Model
	{
		get
		{
			if (_model != null)
				return _model;

			ReadModelAsync().Wait();

			return _model;
		}
	}

	/// <summary>
	/// Reads the model asynchronously.
	/// </summary>
	public virtual async Task ReadModelAsync()
	{
		var handler = Resolver.Resolve<IModelHandler>();

		if (!handler.Processed)
			await handler.ProcessAsync<T>(Resolver);

		_model = handler.GetModel<T>();
	}
}