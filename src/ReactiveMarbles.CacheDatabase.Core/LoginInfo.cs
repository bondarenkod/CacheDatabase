﻿// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveMarbles.CacheDatabase.Core
{
    /// <summary>
    /// Stored login information for a user.
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInfo"/> class.
        /// </summary>
        /// <param name="username">The username for the entry.</param>
        /// <param name="password">The password for the user.</param>
        public LoginInfo(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInfo"/> class.
        /// </summary>
        /// <param name="usernameAndLogin">A username and password stored in a tuple.</param>
        internal LoginInfo((string UserName, string Password) usernameAndLogin)
            : this(usernameAndLogin.UserName, usernameAndLogin.Password)
        {
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; }
    }
}
