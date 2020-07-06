﻿using System;
using System.Linq;
using Couchbase.Linq.Filters;

namespace Couchbase.Linq
{
    /// <summary>
    /// Provides a single point of entry to a Couchbase bucket which makes it easier to compose
    /// and execute queries and to group together changes which will be submitted back into the bucket.
    /// </summary>
    public class BucketContext : IBucketContext
    {
        /// <summary>
        /// Creates a new BucketContext for a given Couchbase bucket.
        /// </summary>
        /// <param name="bucket">Bucket referenced by the new BucketContext.</param>
        public BucketContext(IBucket bucket)
        {
            Bucket = bucket;
        }

        /// <inheritdoc />
        public IBucket Bucket { get; }

        /// <inheritdoc />
        public TimeSpan? QueryTimeout { get; set; }

        /// <inheritdoc />
        public IQueryable<T> Query<T>() =>
            Query<T>(BucketQueryOptions.None);

        /// <inheritdoc />
        public IQueryable<T> Query<T>(BucketQueryOptions options)
        {
            IQueryable<T> query = new CollectionQueryable<T>(Bucket.DefaultCollection(), QueryTimeout);

            if ((options & BucketQueryOptions.SuppressFilters) == BucketQueryOptions.None)
            {
                query = DocumentFilterManager.ApplyFilters(query);
            }

            return query;
        }
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2015 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion
