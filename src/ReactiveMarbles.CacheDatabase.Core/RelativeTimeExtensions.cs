﻿// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive;
using System.Text;

namespace ReactiveMarbles.CacheDatabase.Core
{
    /// <summary>
    /// A set of extension methods that assist with setting expiration times
    /// based on increments from the current time.
    /// </summary>
    public static class RelativeTimeExtensions
    {
        /// <summary>
        /// Inserts a item into the cache.
        /// </summary>
        /// <param name="blobCache">The blob cache to insert the item into.</param>
        /// <param name="key">The key to associate with the entry.</param>
        /// <param name="data">The data for the entry.</param>
        /// <param name="expiration">A timespan that will be added to the current DateTime.</param>
        /// <returns>A observable which will signal when the item is added.</returns>
        public static IObservable<Unit> Insert(this IBlobCache blobCache, string key, byte[] data, TimeSpan expiration)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            return blobCache.Insert(key, data, blobCache.Scheduler.Now + expiration);
        }

        /// <summary>
        /// Inserts a item into the cache.
        /// </summary>
        /// <param name="blobCache">The blob cache to insert the item into.</param>
        /// <param name="key">The key to associate with the entry.</param>
        /// <param name="value">The data for the entry.</param>
        /// <param name="expiration">A timespan that will be added to the current DateTime.</param>
        /// <typeparam name="T">The type of item to insert.</typeparam>
        /// <returns>A observable which will signal when the item is added.</returns>
        public static IObservable<Unit> InsertObject<T>(this IBlobCache blobCache, string key, T value, TimeSpan expiration)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            return blobCache.InsertObject(key, value, blobCache.Scheduler.Now + expiration);
        }

        /// <summary>
        /// Downloads the specified url if there is not already a entry in the cache.
        /// </summary>
        /// <param name="blobCache">The blob cache to insert the item into.</param>
        /// <param name="url">The URL to download if not already in the cache.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="expiration">A timespan that will be added to the current DateTime.</param>
        /// <param name="headers">The headers to specify when getting the entry.</param>
        /// <param name="fetchAlways">If we should fetch always and not return the cache entry if available.</param>
        /// <returns>A observable which will signal when the data is available.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, string url, HttpMethod httpMethod, TimeSpan expiration, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            return blobCache.DownloadUrl(url, httpMethod, headers, fetchAlways, blobCache.Scheduler.Now + expiration);
        }

        /// <summary>
        /// Downloads the specified url if there is not already a entry in the cache.
        /// </summary>
        /// <param name="blobCache">The blob cache to insert the item into.</param>
        /// <param name="url">The URL to download if not already in the cache.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="expiration">A timespan that will be added to the current DateTime.</param>
        /// <param name="headers">The headers to specify when getting the entry.</param>
        /// <param name="fetchAlways">If we should fetch always and not return the cache entry if available.</param>
        /// <returns>A observable which will signal when the data is available.</returns>
        public static IObservable<byte[]> DownloadUrl(this IBlobCache blobCache, Uri url, HttpMethod httpMethod, TimeSpan expiration, IEnumerable<KeyValuePair<string, string>>? headers = null, bool fetchAlways = false)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            return blobCache.DownloadUrl(url, httpMethod, headers, fetchAlways, blobCache.Scheduler.Now + expiration);
        }

        /// <summary>
        /// Saves a username and password.
        /// </summary>
        /// <param name="blobCache">The blob cache to insert the item into.</param>
        /// <param name="user">The username to store.</param>
        /// <param name="password">The password to store.</param>
        /// <param name="host">The host to store against.</param>
        /// <param name="expiration">A timespan that will be added to the current DateTime.</param>
        /// <returns>A observable which will signal when the item is added.</returns>
        public static IObservable<Unit> SaveLogin(this ISecureBlobCache blobCache, string user, string password, string host, TimeSpan expiration)
        {
            if (blobCache is null)
            {
                throw new ArgumentNullException(nameof(blobCache));
            }

            return blobCache.SaveLogin(user, password, host, blobCache.Scheduler.Now + expiration);
        }
    }
}
