/*
 * #%L
 * QueryExample.cs
 * ---
 * Copyright (C) 2000 - 2017 Oracle and/or its affiliates. All rights reserved.
 * ---
 * Oracle is a registered trademarks of Oracle Corporation and/or its
 * affiliates.
 * 
 * This software is the confidential and proprietary information of Oracle
 * Corporation. You shall not disclose such confidential and proprietary
 * information and shall use it only in accordance with the terms of the
 * license agreement you entered into with Oracle.
 * 
 * This notice may not be removed or altered.
 * #L%
 */

using System;
using System.Collections;
using System.Text;

using Tangosol.Examples.Pof;
using Tangosol.Net;
using Tangosol.Net.Cache;
using Tangosol.Util.Aggregator;
using Tangosol.Util.Extractor;
using Tangosol.Util.Filter;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// QueryExample runs sample queries for contacts.
    /// </summary>
    /// <remarks>
    /// The purpose of this example is to show how to create Extractors on 
    /// cache data and how to create a KeyExtractor for the cache keys. It also
    /// illustrates how to use the indexes to filter the dataset in order to 
    /// efficiently create a matching set. Finally the example demonstrates how 
    /// to use some of the built-in cache aggregators to do simple 
    /// computational tasks on the cache data.
    /// </remarks>
    /// <author>dag  2009.03.26</author>
    public class QueryExample
    {
        #region Methods

        /// <summary>
        /// Create indexes in the cache and query it for data.
        /// </summary>
        /// <param name="cache">
        /// The target cache to query.
        /// </param>
        public virtual void Query(INamedCache cache)
        {
            Console.WriteLine("------QueryExample begins------");
            // Add indexes to make queries more efficient
            // Ordered index applied to fields used in range and like filter queries
            cache.AddIndex(new ReflectionExtractor("getAge"), /*fOrdered*/ true,
                /*comparator*/ null);
            cache.AddIndex(new KeyExtractor(new ReflectionExtractor("getLastName")),
                /*fOrdered*/ true, /*comparator*/ null);
            cache.AddIndex(new ChainedExtractor("getHomeAddress.getCity"),
                /*fOrdered*/ true, /*comparator*/ null);
            cache.AddIndex(new ChainedExtractor("getHomeAddress.getState"),
                /*fOrdered*/ false, /*comparator*/ null);
            cache.AddIndex(new ChainedExtractor("getWorkAddress.getState"),
                /*fOrdered*/ false, /*comparator*/ null);

            // Find all contacts who live in Massachusetts
            ICacheEntry[] aCacheEntry = cache.GetEntries(new EqualsFilter(
                    "getHomeAddress.getState", "MA"));
            PrintResults("MA Residents", aCacheEntry);

            // Find all contacts who live in Massachusetts and work elsewhere
            aCacheEntry = cache.GetEntries(new AndFilter(
                    new EqualsFilter("getHomeAddress.getState", "MA"),
                    new NotEqualsFilter("getWorkAddress.getState", "MA")));
            PrintResults("MA Residents, Work Elsewhere", aCacheEntry);

            // Find all contacts whose city name begins with 'S'
            aCacheEntry = cache.GetEntries(
                new LikeFilter("getHomeAddress.getCity", "S%"));
            PrintResults("City Begins with S", aCacheEntry);

            int nAge = 58;
            // Find all contacts who are older than nAge
            aCacheEntry = cache.GetEntries(new GreaterFilter("getAge", nAge));
            PrintResults("Age > " + nAge, aCacheEntry);

            // Find all contacts with last name beginning with 'S' that live
            // in Massachusetts. Uses both key and value in the query.
            aCacheEntry = cache.GetEntries(new AndFilter(
                    new LikeFilter(new KeyExtractor("getLastName"), "S%",
                                   (char) 0, false),
                    new EqualsFilter("getHomeAddress.getState", "MA")));
            PrintResults("Last Name Begins with S and State Is MA", aCacheEntry);

            // Count contacts who are older than nAge
            Console.WriteLine("count > " + nAge + ": "+ cache.Aggregate(
                    new GreaterFilter("getAge", nAge), new Count()));

            // Find minimum age
            Console.WriteLine("min age: " + cache.Aggregate(
                AlwaysFilter.Instance, new LongMin("getAge")));

            // Calculate average age
            Console.WriteLine("avg age: " + cache.Aggregate(
                AlwaysFilter.Instance, new DoubleAverage("getAge")));

            // Find maximum age
            Console.WriteLine("max age: " + cache.Aggregate(
                AlwaysFilter.Instance, new LongMax("getAge")));
            Console.WriteLine("------QueryExample completed------");
        }
        
        #endregion

        #region Helper methods

        /// <summary>
        /// Print results of the query.
        /// </summary>
        /// <param name="sTitle">
        /// The title that describes the results.
        /// </param>
        /// <param name="cacheEntries">
        /// A set of query results.
        /// </param>
        protected void PrintResults(String sTitle, ICacheEntry[] cacheEntries)
        {
            Console.WriteLine(sTitle);
            foreach (ICacheEntry cacheEntry in cacheEntries)
            {
                Console.WriteLine(cacheEntry);
            }
        }

        #endregion
    }
}
