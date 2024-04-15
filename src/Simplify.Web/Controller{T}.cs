using System.Threading.Tasks;
using Simplify.Web.Model;

#nullable disable

namespace Simplify.Web;

/// <summary>
/// Provides the user synchronous model controllers base class version 1.
/// </summary>
public abstract class Controller<T> : SyncControllerBase
	where T : class
{
	private T _model;

	/// <summary>
	/// Gets the model (and reads it synchronously, if it is null) of a current request.
	/// </summary>
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

	internal virtual IModelHandler ModelHandler { get; set; } = null!;

	/// <summary>
	/// Reads the model asynchronously.
	/// </summary>
	public virtual async Task ReadModelAsync()
	{
		if (!ModelHandler.Processed)
			await ModelHandler.ProcessAsync<T>();

		_model = ModelHandler.GetModel<T>();
	}
}