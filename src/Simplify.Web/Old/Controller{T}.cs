﻿#nullable disable

using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Old.Model;

namespace Simplify.Web.Old;

/// <summary>
/// User synchronous model controllers base class version 1.
/// </summary>
public abstract class Controller<T> : SyncControllerBase
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