/*
 * #%L
 * QueryLanguageExample.cs
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
﻿
using System;
using System.Collections;
using Tangosol.Net;
using Tangosol.Util;
using Tangosol.Util.Aggregator;

namespace Tangosol.Examples.Contacts    
{
    /// <summary>
    /// QueryLanguageExample runs sample queries for contacts.
    /// </summary>
    /// <remarks>
    /// <p>
    /// The purpose of this example is to show how to create a
    /// ReflectionExtrector on cache data and how to create a KeyExtractor
    /// index for the cache keys.</p>
    /// It also illustrates how to utilize the indexes to filter the data set
    /// how to efficiently create a matching set.<p/>
    /// <p>
    /// Finally the example demonstrates how to use some of the built-in
    /// cache aggregators to do simple computational tasks on the cache data.</p>
    /// </remarks>
    /// <author>Ivan Cikic  2010.03.09</author>
    public class QueryLanguageExample
    {
        /// <summary>
        /// Create indexes in the cache and query it for data.
        /// </summary>
        /// <param name="cache">
        /// Cache to query.
        /// </param>
        /// <param name="ff">
        /// The <see cref="FilterFactory"/> used to convert string to
        /// <b>IFilters</b>.
        /// </param>
        public void Query(INamedCache cache, FilterFactory ff)
        {
            Console.WriteLine("------QueryLanguageExample begins------");

            // Add indexes to make queries more efficient
            // Ordered index applied to fields used in range and like filter queries
            cache.AddIndex(ff.CreateExtractor("age"), /*fOrdered*/ true, /*comparator*/ null);
            cache.AddIndex(ff.CreateExtractor("key().lastName"), /*fOrdered*/ true, /*comparator*/ null);
            cache.AddIndex(ff.CreateExtractor("homeAddress.city"), /*fOrdered*/ true, /*comparator*/ null);
            cache.AddIndex(ff.CreateExtractor("homeAddress.state"), /*fOrdered*/ false, /*comparator*/ null);
            cache.AddIndex(ff.CreateExtractor("workAddress.state"), /*fOrdered*/ false, /*comparator*/ null);

            // Find all contacts who live in Massachusetts
            ICollection results = cache.GetEntries(ff.CreateFilter("homeAddress.state = 'MA'"));
            PrintResults("MA Residents", results);

            // Find all contacts who live in Massachusetts and work elsewhere
            results = cache.GetEntries(ff.CreateFilter("homeAddress.state is 'MA' and workAddress is not 'MA'"));
            PrintResults("MA Residents, Work Elsewhere", results);

            // Find all contacts whose city name begins with 'S'
            results = cache.GetEntries(ff.CreateFilter("homeAddress.city like 'S%'"));
            PrintResults("City Begins with S", results);

            const int age = 58;
            object[]  env =  new object[] { age };
            // Find all contacts who are older than nAge
            results = cache.GetEntries(ff.CreateFilter("age > ?1", env));
            PrintResults("Age > " + age, results);

            // Find all contacts with last name beginning with 'S' that live
            // in Massachusetts. Uses both key and value in the query
            results = cache.GetEntries(ff.CreateFilter("lastName like 'S%' and homeAddress.state = 'MA'"));
            PrintResults("Last Name Begins with S and State Is MA", results);
            // Count contacts who are older than nAge for the entire cache dataset.
            Console.WriteLine("count > {0} : {1}", age, 
                cache.Aggregate(ff.CreateFilter("age > ?1", env), new Count()));

            // Find minimum age for the entire cache dataset.
            IFilter always = ff.CreateFilter("true");
            Console.WriteLine("min age: {0}", cache.Aggregate(always, new LongMin("getAge")));

            // Calculate average age for the entire cache dataset.
            Console.WriteLine("avg age: {0}", cache.Aggregate(always, new DoubleAverage("getAge")));

            // Find maximum age for the entire cache dataset.
            Console.WriteLine("max age: {0}", cache.Aggregate(always, new LongMax("getAge")));

            Console.WriteLine("------QueryLanguageExample completed------");
        }

        /// <summary>
        /// Print results of the query.
        /// </summary>
        /// <param name="title">The title that describes the results</param>
        /// <param name="results">A set of query results.</param>
        private static void PrintResults(string title, ICollection results)
        {
            Console.WriteLine(title);
            foreach (object result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
