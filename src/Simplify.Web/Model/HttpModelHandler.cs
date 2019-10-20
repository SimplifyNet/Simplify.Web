using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Validation;
using Simplify.Web.Modules;

namespace Simplify.Web.Model
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

			typeof (ValidationAttributesExecutor)
		};

		/// <summary>
		/// Adds the model binder into model binders list, this type should be registered in Simplify.DI container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void RegisterModelBinder<T>()
			where T : IModelBinder
		{
			ModelBindersTypes.Add(typeof(T));
		}

		/// <summary>
		/// Adds the model validator into model validators list, this type should be registered in Simplify.DI container.
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
		/// <param name="resolver">The resolver.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException">Unrecognized request content type for binding: {_context.Request.ContentType}</exception>
		public T Process<T>(IDIResolver resolver)
		{
			var args = new ModelBinderEventArgs<T>(_context);

			foreach (var binder in ModelBindersTypes.Select(binderType => (IModelBinder)resolver.Resolve(binderType)))
			{
				binder.Bind(args);

				if (!args.IsBound)
					continue;

				Validate(args, resolver);

				return args.Model;
			}

			throw new ModelBindingException($"Unrecognized request content type for binding: {_context.Request.ContentType}");
		}

		private static void Validate<T>(ModelBinderEventArgs<T> args, IDIResolver resolver)
		{
			foreach (var validator in ModelValidatorsTypes.Select(x => (IModelValidator)resolver.Resolve(x)))
				validator.Validate(args.Model, resolver);
		}
	}
}