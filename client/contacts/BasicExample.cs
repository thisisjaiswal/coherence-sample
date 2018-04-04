/*
 * #%L
 * BasicExample.cs
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
using System.Collections.Specialized;
using System.Text;

using Tangosol.Examples.Pof;
using Tangosol.Net;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// BasicExample shows basic cache methods like adding, getting and 
    /// removing data.
    /// </summary>
    /// <author>dag  2009.03.26</author>
    public class BasicExample
    {
        #region Methods

        /// <summary>
        /// Execute a cycle of basic operations.
        /// </summary>
        /// <param name="cache">
        /// The target cache.
        /// </param>
        public virtual void Execute(INamedCache cache)
        {
            Contact contact = createContact();
            ContactId contactId = new ContactId(contact.FirstName,
                    contact.LastName);
            
            Console.WriteLine("------BasicExample begins------");
            // associate a ContactId with a Contact in the cache
            cache.Add(contactId, contact);
            
            // retrieve the Contact associated with a ContactId from the cache
            contact = (Contact)cache[contactId];

            // remove mapping of ContactId to Contact from the cache.
            cache.Remove(contactId);
            Console.WriteLine("------BasicExample completed------");
        }

        protected virtual Contact createContact()
        {
            IDictionary dictPhone = new ListDictionary();

            dictPhone.Add("home", new PhoneNumber(11, 1, 617, 5551212));

            return new Contact("John", "Bigboote", new Address("10 My St.", "", 
                "Boston", "MA", "01111", "US"), null, dictPhone, 
                new DateTime(1940, 6, 24));
        }

        #endregion
    
    }
}
