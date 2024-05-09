using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Old.Util;

namespace Simplify.Web.Old.Core.StaticFiles;

/// <summary>
/// Provides static file handler.
/// </summary>
/// <seealso cref="IStaticFileHandler" />
/// <remarks>
/// Initializes a new instance of the <see cref="StaticFileHandler"/> class.
/// </remarks>
/// <param name="staticFilesPaths">The static files paths.</param>
/// <param name="sitePhysicalPath">The site physical path.</param>
public class StaticFileHandler(IList<string> staticFilesPaths, string sitePhysicalPath) : IStaticFileHandler
{
	/// <summary>
	/// Determines whether  relative file path is a static file route path.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public bool IsStaticFileRoutePath(string relativeFilePath) =>
		staticFilesPaths
			.Where(relativeFilePath.ToLower().StartsWith)
			.Any(_ => File.Exists(sitePhysicalPath + relativeFilePath));

	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	/// <param name="headers">The headers.</param>
	/// <returns></returns>
	public DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers) => HttpRequestUtil.GetIfModifiedSinceTime(headers);

	/// <summary>
	/// Determines whether file can be used from cached.
	/// </summary>
	/// <param name="cacheControlHeader">The cache control header.</param>
	/// <param name="ifModifiedSinceHeader">If modified since header.</param>
	/// <param name="fileLastModifiedTime">The file last modified time.</param>
	/// <returns></returns>
	public bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime) =>
		!HttpRequestUtil.IsNoCacheRequested(cacheControlHeader)
			&& ifModifiedSinceHeader != null
			&& fileLastModifiedTime <= ifModifiedSinceHeader.Value;

	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	/// <param name="request">The request.</param>
	/// <returns></returns>
	public string GetRelativeFilePath(HttpRequest request) => HttpRequestUtil.GetRelativeFilePath(request);

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public DateTime GetFileLastModificationTime(string relativeFilePath) => DateTimeOperations.TrimMilliseconds(File.GetLastWriteTimeUtc(relativeFilePath));

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public async Task<byte[]> GetFileData(string relativeFilePath)
	{
		using var stream = File.Open(relativeFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		var result = new byte[stream.Length];

		await stream.ReadAsync(result, 0, (int)stream.Length);

		return result;
	}
}