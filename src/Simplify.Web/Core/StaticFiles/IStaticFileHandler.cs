using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Represent static files handler
	/// </summary>
	public interface IStaticFileHandler
	{
		/// <summary>
		/// Determines whether  relative file path is a static file route path.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		bool IsStaticFileRoutePath(string relativeFilePath);

		/// <summary>
		/// Gets If-Modified-Since time header from headers collection.
		/// </summary>
		/// <param name="headers">The headers.</param>
		/// <returns></returns>
		DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers);

		/// <summary>
		/// Determines whether file can be used from cached.
		/// </summary>
		/// <param name="cacheControlHeader">The cache control header.</param>
		/// <param name="ifModifiedSinceHeader">If modified since header.</param>
		/// <param name="fileLastModifiedTime">The file last modified time.</param>
		/// <returns></returns>
		bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime);

		/// <summary>
		/// Gets the relative file path of request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		string GetRelativeFilePath(HttpRequest request);

		/// <summary>
		/// Gets the file last modification time.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		DateTime GetFileLastModificationTime(string relativeFilePath);

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		Task<byte[]> GetFileData(string relativeFilePath);
	}
}