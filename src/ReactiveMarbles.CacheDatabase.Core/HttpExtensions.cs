﻿// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Subjects;

namespace ReactiveMarbles.CacheDatabase.Core
{
    /// <summary>
    /// Extension methods for handling http operations.
    /// </summary>
    public static class HttpExtensions
    {
        private static IHttpService HttpService => CoreRegistrations.HttpService ?? throw new InvalidOperationException("Unable to resolve IHttpService, make sure you are including the correct CacheDatabase NuGet packages.");

        /// <summary>
        /// Writes to a stream and returns a observable.
        /// </summary>
        /// <param name="blobCache">The stream to write to.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="start">The start location where to write from.</param>
        /// <param name="length">The length in bytes to read.</param>
        /// <returns>An observable that signals when the write operation has completed.</returns>
        public static IObservable<Unit> WriteAsyncRx(this Stream blobCache, byte[] data, int start, int length)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            var ret = new AsyncSubject<Unit>();

            try
            {
                blobCache.BeginWrite(
                    data,
                    start,
                    length,
                    result =>
                    {
                        try
                        {
                            blobCache.EndWrite(result);
                            ret.OnNext(Unit.Default);
                            ret.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            ret.OnError(ex);
                        }
                    },
                    null);
            }
            catch (Exception ex)
            {
                ret.OnError(ex);
            }

            return ret;
        }

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. The URL itself is used as the key.
        /// </summary>
        /// <param name="blobCache">The blob cache to perform the operation on.</param>
        /// <param name="url">The URL to download.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, string url, HttpMethod? method = null, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null) =>
            HttpService.DownloadUrl(blobCache, new Uri(url), method, headers, fetchAlways, absoluteExpiration);

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. The URL itself is used as the key.
        /// </summary>
        /// <param name="blobCache">The blob cache to perform the operation on.</param>
        /// <param name="url">The URL to download.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, Uri url, HttpMethod? method = null, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null) =>
            HttpService.DownloadUrl(blobCache, url, method, headers, fetchAlways, absoluteExpiration);

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. An explicit key is provided rather than the URL itself.
        /// </summary>
        /// <param name="blobCache">The blob cache to perform the operation on.</param>
        /// <param name="key">The key to store with.</param>
        /// <param name="url">The URL to download.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, string key, string url, HttpMethod? method = null, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null) =>
            HttpService.DownloadUrl(blobCache, key, new Uri(url), method, headers, fetchAlways, absoluteExpiration);

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. An explicit key is provided rather than the URL itself.
        /// </summary>
        /// <param name="blobCache">The blob cache to perform the operation on.</param>
        /// <param name="key">The key to store with.</param>
        /// <param name="url">The URL to download.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, string key, Uri url, HttpMethod? method = null, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null) =>
            HttpService.DownloadUrl(blobCache, key, url, method, headers, fetchAlways, absoluteExpiration);
    }
}
