﻿// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace ReactiveMarbles.CacheDatabase.Core
{
    /// <summary>
    /// Extension methods handling login use cases.
    /// </summary>
    public static class LoginExtensions
    {
        /// <summary>
        /// Save a user/password combination in a secure blob cache. Note that
        /// this method only allows exactly *one* user/pass combo to be saved,
        /// calling this more than once will overwrite the previous entry.
        /// </summary>
        /// <param name="blobCache">The blob cache where to store the data.</param>
        /// <param name="user">The user name to save.</param>
        /// <param name="password">The associated password.</param>
        /// <param name="host">The host to associate with the data.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>A observable which signals when the insert is completed.</returns>
        public static IObservable<Unit> SaveLogin(this ISecureBlobCache blobCache, string user, string password, string host = "default", DateTimeOffset? absoluteExpiration = null)
        {
            return blobCache.InsertObject("login:" + host, new Tuple<string, string>(user, password), absoluteExpiration);
        }

        /// <summary>
        /// Returns the currently cached user/password. If the cache does not
        /// contain a user/password, this returns an Observable which
        /// OnError's with KeyNotFoundException.
        /// </summary>
        /// <param name="blobCache">The blob cache where to get the data.</param>
        /// <param name="host">The host associated with the data.</param>
        /// <returns>A Future result representing the user/password Tuple.</returns>
        public static IObservable<LoginInfo> GetLoginAsync(this ISecureBlobCache blobCache, string host = "default")
        {
            return blobCache.GetObject<(string, string)>("login:" + host).Select(x => new LoginInfo(x));
        }

        /// <summary>
        /// Erases the login associated with the specified host.
        /// </summary>
        /// <param name="blobCache">The blob cache where to erase the data.</param>
        /// <param name="host">The host associated with the data.</param>
        /// <returns>A observable which signals when the erase is completed.</returns>
        public static IObservable<Unit> EraseLogin(this ISecureBlobCache blobCache, string host = "default")
        {
            return blobCache.InvalidateObject<(string, string)>("login:" + host);
        }
    }
}
