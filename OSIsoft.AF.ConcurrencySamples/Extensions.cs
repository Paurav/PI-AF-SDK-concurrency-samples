#region Copyright
//  Copyright 2016  OSIsoft, LLC
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
#endregion


using System;
using System.Threading;
using System.Threading.Tasks;
using OSIsoft.AF;

namespace Samples
{
    public static class Extensions
    {
        public static T Find<T>(this PISystems systems, string path)
            where T : AFObject
        {
            if (systems.DefaultPISystem == null)
            {
                throw new InvalidOperationException("Default PISystem must be set.");
            }

            return AFObject.FindObject(path, systems.DefaultPISystem) as T;
        }

        public static Task RunConcurrent(
            this ConcurrentExclusiveSchedulerPair schedulerPair,
            Action action)
        {
            return RunActionOnScheduler(schedulerPair.ConcurrentScheduler, action);
        }

        public static Task RunExclusive(
            this ConcurrentExclusiveSchedulerPair schedulerPair,
            Action action)
        {
            return RunActionOnScheduler(schedulerPair.ExclusiveScheduler, action);
        }

        private static Task RunActionOnScheduler(TaskScheduler scheduler, Action action)
        {
            return Task.Factory.StartNew(
                action,
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                scheduler);
        }
    }
}
