﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Represents a Simplify.Web request handling pipeline handler (stage)
/// </summary>
public interface IRequestHandler
{
	/// <summary>
	/// Handles the requests.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	Task Handle(HttpContext context, RequestHandlerAsync next);
}