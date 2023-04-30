using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Microsoft.Owin;
using Simplify.Web.Owin;
using Simplify.Web.Util;

#nullable disable

namespace Simplify.Web.Core.StaticFiles;

/// <summary>
/// Provides static file handler
/// </summary>
/// <seealso cref="IStaticFileHandler" />
public class StaticFileHandler : IStaticFileHandler
{
	private static IFileSystem _fileSystemInstance;

	private readonly IList<string> _staticFilesPaths;
	private readonly string _sitePhysicalPath;

	/// <summary>
	/// Initializes a new instance of the <see cref="StaticFileHandler"/> class.
	/// </summary>
	/// <param name="staticFilesPaths">The static files paths.</param>
	/// <param name="sitePhysicalPath">The site physical path.</param>
	public StaticFileHandler(IList<string> staticFilesPaths, string sitePhysicalPath)
	{
		_staticFilesPaths = staticFilesPaths;
		_sitePhysicalPath = sitePhysicalPath;
	}

	/// <summary>
	/// Gets or sets the file system.
	/// </summary>
	/// <value>
	/// The file system.
	/// </value>
	/// <exception cref="ArgumentNullException"></exception>
	public static IFileSystem FileSystem
	{
		get { return _fileSystemInstance ?? (_fileSystemInstance = new FileSystem()); }

		set
		{
			if (value == null)
				throw new ArgumentNullException();

			_fileSystemInstance = value;
		}
	}

	/// <summary>
	/// Determines whether  relative file path is a static file route path.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public bool IsStaticFileRoutePath(string relativeFilePath)
	{
		return _staticFilesPaths.Where(relativeFilePath.ToLower().StartsWith).Any(path => FileSystem.File.Exists(_sitePhysicalPath + relativeFilePath));
	}

	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	/// <param name="headers">The headers.</param>
	/// <returns></returns>
	public DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers)
	{
		return OwinHttpRequestHelper.GetIfModifiedSinceTime(headers);
	}

	/// <summary>
	/// Determines whether file can be used from cached.
	/// </summary>
	/// <param name="cacheControlHeader">The cache control header.</param>
	/// <param name="ifModifiedSinceHeader">If modified since header.</param>
	/// <param name="fileLastModifiedTime">The file last modified time.</param>
	/// <returns></returns>
	public bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime)
	{
		return !OwinHttpRequestHelper.IsNoCacheRequested(cacheControlHeader) && ifModifiedSinceHeader != null &&
		       fileLastModifiedTime <= ifModifiedSinceHeader.Value;
	}

	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	/// <param name="request">The request.</param>
	/// <returns></returns>
	public string GetRelativeFilePath(IOwinRequest request)
	{
		return OwinHttpRequestHelper.GetRelativeFilePath(request);
	}

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public DateTime GetFileLastModificationTime(string relativeFilePath)
	{
		return DateTimeOperations.TrimMilliseconds(FileSystem.File.GetLastWriteTimeUtc(relativeFilePath));
	}

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	public byte[] GetFileData(string relativeFilePath)
	{
		return FileSystem.File.ReadAllBytes(_sitePhysicalPath + relativeFilePath);
	}
}