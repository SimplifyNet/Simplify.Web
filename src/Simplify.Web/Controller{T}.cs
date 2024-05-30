using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Model;

#nullable disable

namespace Simplify.Web;

/// <summary>
/// Provides the user synchronous model controllers base class version 1.
/// </summary>
/// <typeparam name="T">The model type</typeparam>
/// <seealso cref="SyncControllerBase" />
public abstract class Controller<T> : SyncControllerBase
	where T : class
{
	private T _model;

	/// <summary>
	/// Gets the model (and reads it synchronously, if it is null) of a current request.
	/// </summary>
	/// <value>
	/// The model.
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
			await handler.ProcessAsync<T>();

		_model = handler.GetModel<T>();
	}
}