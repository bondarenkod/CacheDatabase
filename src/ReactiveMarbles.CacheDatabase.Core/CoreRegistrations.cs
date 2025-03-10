﻿// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Text;

namespace ReactiveMarbles.CacheDatabase.Core
{
    /// <summary>
    /// Registrations needed for running the application.
    /// </summary>
    public static class CoreRegistrations
    {
        private static IScheduler? _taskPoolOverride;

        /// <summary>
        /// Gets or sets the serializer.
        /// </summary>
        public static ISerializer? Serializer { get; set; }

        /// <summary>
        /// Gets or sets the http service.
        /// </summary>
        public static IHttpService? HttpService { get; set; } = new HttpService();

        /// <summary>
        /// Gets or sets the Scheduler used for task pools.
        /// </summary>
        public static IScheduler TaskpoolScheduler
        {
            get => _taskPoolOverride ?? TaskPoolScheduler.Default;
            set => _taskPoolOverride = value;
        }
    }
}
