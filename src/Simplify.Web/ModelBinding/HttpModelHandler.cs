using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.ModelBinding.Binders;
using Simplify.Web.ModelBinding.Validation;
using Simplify.Web.Modules;

namespace Simplify.Web.ModelBinding
{
	/// <summary>
	/// Provides model handling
	/// </summary>
	public class HttpModelHandler : IModelHandler
	{
		private readonly IWebContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpModelHandler"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public HttpModelHandler(IWebContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets the model binders types.
		/// </summary>
		/// <value>
		/// The model binders types.
		/// </value>
		// ReSharper disable once ConvertToAutoProperty
		public static IList<Type> ModelBindersTypes { get; } = new List<Type>
		{
			// Default model binders

			typeof (HttpQueryModelBinder),
			typeof (HttpFormModelBinder)
		};

		/// <summary>
		/// Gets the model validators types.
		/// </summary>
		/// <value>
		/// The model validators types.
		/// </value>
		// ReSharper disable once ConvertToAutoProperty
		public static IList<Type> ModelValidatorsTypes { get; } = new List<Type>
		{
			// Default model validators

			typeof (ObjectPropertiesValidator)
		};

		/// <summary>
		/// Registers the model binder.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void RegisterModelBinder<T>()
			where T : IModelBinder
		{
			ModelBindersTypes.Add(typeof(T));
		}

		/// <summary>
		/// Registers the model validator.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void RegisterModelValidator<T>()
			where T : IModelValidator
		{
			ModelValidatorsTypes.Add(typeof(T));
		}

		/// <summary>
		/// Parses object from HTTP data and validates it
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public T Process<T>()
		{
			var args = new ModelBinderEventArgs<T>(_context);

			foreach (var binder in ModelBindersTypes.Select(binderType => (IModelBinder)Activator.CreateInstance(binderType)))
			{
				binder.Bind(args);

				if (!args.IsBinded)
					continue;

				Validate(args);

				return args.Model;
			}

			throw new ModelBindingException($"Unrecognized request content type for binding: {_context.Request.ContentType}");
		}

		private static void Validate<T>(ModelBinderEventArgs<T> args)
		{
			foreach (var validator in ModelValidatorsTypes.Select(x => (IModelValidator)Activator.CreateInstance(x)))
				validator.Validate(args.Model);
		}
	}
}