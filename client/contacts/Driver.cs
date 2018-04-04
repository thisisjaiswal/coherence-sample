/*
 * #%L
 * Driver.cs
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
using System.IO;

using Tangosol.Examples;
using Tangosol.Net;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// Driver executes all the contact examples.
    /// </summary>
    public static class Driver
    {
        /// <summary>
        /// Execute all the contact examples.
        /// </summary>
        /// <remarks>
        /// usage: [cache-name] [contacts-file]
        /// </remarks>
        /// <param name="asArg">
        /// Command line arguments.
        /// </param>
        public static void Main(string[] asArg)
        {
            string sCache = asArg.Length > 0 ? asArg[0] :
                LoaderExample.CACHENAME;
            string sFile  = asArg.Length > 1 ? asArg[1] :
                "C:\\Temp\\thisisjaiswal\\example\\client\\contacts.csv";

            INamedCache cache = CacheFactory.GetCache(sCache);

      
            Console.WriteLine("------contacts examples begin------");
            // Load the sample contacts into cache.
            new LoaderExample().Load(cache, new StreamReader(sFile));

            // Run sample queries.
            new QueryExample().Query(cache);

            // Run sample queries using query language.
            new QueryLanguageExample().Query(cache, new FilterFactory("InvocationService"));

            // Run sample change observer.
            ObserverExample observer = new ObserverExample();
            observer.Observe(cache);

            // Run basic cache commands.
            new BasicExample().Execute(cache);

            // Run sample entry processor.
            new ProcessorExample().Execute(cache);

            // Stop observing
            observer.Remove(cache);
            
            CacheFactory.Shutdown();
            Console.WriteLine("------contacts examples completed------");
        }
    }
}
