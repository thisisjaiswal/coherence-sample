/*
 * #%L
 * ProcessorExample.cs
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
using System.Text;

using Tangosol.Examples.Pof;
using Tangosol.Net;
using Tangosol.Util.Filter;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// ProcessorExample demonstrates how to use a processor to modify data in 
    /// the cache. All Contacts who live in MA will have their work address 
    /// updated.
    /// </summary>
    /// <author>dag  2009.03.26</author>
    public class ProcessorExample
    {
        #region Methods

        /// <summary>
        /// Perform the example update to contacts.
        /// </summary>
        /// <param name="cache">
        /// The target cache.
        /// </param>
        public virtual void Execute(INamedCache cache)
        {
            Console.WriteLine("------ProcessorExample begins------");
            // People who live in Massachusetts moved to an in-state office
            Address addrWork = new Address("200 Newbury St.", "Yoyodyne, Ltd.",
                    "Boston", "MA", "02116", "US");

            // Apply the OfficeUpdater on all contacts who live in MA 
            cache.InvokeAll(new EqualsFilter("getHomeAddress.getState", "MA"),
                    new OfficeUpdater(addrWork));
            Console.WriteLine("------ProcessorExample completed------");
        }

        #endregion
    }
}
