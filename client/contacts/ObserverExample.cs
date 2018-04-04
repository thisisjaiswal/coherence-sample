/*
 * #%L
 * ObserverExample.cs
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
using Tangosol.Net.Cache;
using Tangosol.Net.Cache.Support;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// ObserverExample demonstrates how to use a MapListener to monitor cache 
    /// events.
    /// </summary>
    /// <author>dag  2009.03.26</author>
    public class ObserverExample
    {
        #region Methods

        /// <summary>
        /// Observe changes to the contacts.
        /// </summary>
        /// <param name="cache">target cache</param>
        public virtual void Observe(INamedCache cache)
        {
            Console.WriteLine("------ObserverExample begins------");
            m_listener = new ContactChangeListener();
            cache.AddCacheListener(m_listener);
            Console.WriteLine("------ContactChangleListener added------");
        }

        /// <summary>
        /// Stop observing changes to the contacts
        /// </summary>
        /// <param name="cache">target cache</param>
        public virtual void Remove(INamedCache cache)
        {
            cache.RemoveCacheListener(m_listener);
            Console.WriteLine("------ContactChangeListener removed------");
            Console.WriteLine("------ObserverExample completed------");
        }

        #endregion

        #region ContactChangeListener nested class
        
        /// <summary>
        /// ContactChangeListener listens for changes to Contacts.
        /// </summary>
        public class ContactChangeListener : ICacheListener
        {
            #region IMapListener implementation

            /// <see cref="ICacheListener"/>
            public virtual void EntryInserted(CacheEventArgs eventArg)
            {
                Console.WriteLine("entry inserted:");
                Contact contactNew = (Contact)eventArg.NewValue;
                Console.WriteLine(contactNew);
            }
            
            /// <see cref="ICacheListener"/>
            public virtual void EntryUpdated(CacheEventArgs eventArg)
            {
                Console.WriteLine("entry updated");
                Contact contactOld = (Contact) eventArg.OldValue;
                Console.WriteLine("old value:");
                Console.WriteLine(contactOld);
                Contact contactNew = (Contact) eventArg.NewValue;
                Console.WriteLine("new value:");
                Console.WriteLine(contactNew);
            }

            /// <see cref="ICacheListener"/>
            public virtual void EntryDeleted(CacheEventArgs eventArg)
            {
                Console.WriteLine("entry deleted:");
                Contact contactOld = (Contact)eventArg.OldValue;
                Console.WriteLine(contactOld);
            }

            #endregion
        }
        
        #endregion

        #region Data members
        /// <summary>
        /// The MapListener observing changes
        /// </summary>
        private ContactChangeListener m_listener;
        
        #endregion
    }
}
