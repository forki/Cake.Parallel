﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Globalization;
using Cake.Core;

namespace Cake.Parallel.Module
{
    public class ParallelGraphBuilder
    {
        public static CakeGraph Build(List<CakeTask> tasks)
        {
            var graph = new CakeGraph();
            foreach (var task in tasks)
            {
                graph.Add(task.Name);
            }
            foreach (var task in tasks)
            {
                foreach (var dependency in task.Dependencies)
                {
                    if (!graph.Exist(dependency.Name))
                    {
                        const string format = "Task '{0}' is dependent on task '{1}' which does not exist.";
                        var message = string.Format(CultureInfo.InvariantCulture, format, task.Name, dependency);
                        throw new CakeException(message);
                    }

                    graph.Connect(dependency.Name, task.Name);
                }
            }
            return graph;
        }
    }
}
