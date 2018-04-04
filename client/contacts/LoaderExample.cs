/*
 * #%L
 * LoaderExample.cs
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
using System.IO;
using System.Text;

using Tangosol.Examples.Pof;
using Tangosol.Net;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// LoaderExample loads contacts into the cache from a file or stream.
    /// <p/>
    /// Demonstrates the most effective way of inserting data into a cache using the
    /// Map.putAll() method. This will allow for minimizing the number of network
    /// roundtrips between the application and the cache.
    /// </summary>
    /// <author>dag  2009.03.26</author>
    public class LoaderExample
    {
        #region Methods
        
        /// <summary>
        /// Load contacts from the Stream and insert them into the cache.
        /// </summary>
        /// <param name="cache">
        /// The target cache.
        /// </param>
        /// <param name="reader">
        /// The stream containing contacts.
        /// </param>
        /// <throws>
        /// IOException on read error.
        /// </throws>
        public virtual void Load(INamedCache cache, StreamReader reader)
        {
            IDictionary dictBatch = new Hashtable(BATCH_SIZE);
            int                            cContact  = 0;
            Contact                        contact;

            Console.WriteLine("------LoaderExample begins------");
            while ((contact = ReadContact(reader)) != null)
            {
                // Add contact to batch
                dictBatch.Add(new ContactId(contact.FirstName, 
                    contact.LastName), contact);
                ++cContact;
                
                // When reached the BATCH_SIZE threashold transfer the records 
                // to the cache.
                if (cContact % BATCH_SIZE == 0)
                {
                    // minimize the network roundtrips by using InsertAll()
                    cache.InsertAll(dictBatch);
                    dictBatch.Clear();
                    Console.Write('.');
                }
            }

            if (dictBatch.Count > 0)
            {
                // load final batch
                cache.InsertAll(dictBatch);
            }

            Console.WriteLine("Added " + cContact + " entries to cache");
            Console.WriteLine("------LoaderExample completed------");
        }

        /// <summary>
        /// Read a single contact from the supplied stream.
        /// </summary>
        /// <param name="reader">
        /// The stream containing contacts.
        /// </param>
        /// <returns>
        /// The contact or null upon reaching end of stream.
        /// </returns>
        public Contact ReadContact(StreamReader reader)
        {
            string sRecord = reader.ReadLine();

            if (sRecord == null)
            {
                return null;
            }

            // parse record
            char[]    chSeparators = { ',' };
            string[]  asPart       = sRecord.Split(chSeparators);
            int       ofPart       = 0;
            string    sFirstName   = asPart[ofPart++];
            string    sLastName    = asPart[ofPart++];
            DateTime  dtBirth      = DateTime.Parse(asPart[ofPart++]);
            ContactId id           = new ContactId(sFirstName, sLastName);
            Address   addrHome     = new Address(
                /*streetline1*/ asPart[ofPart++],
                /*streetline2*/ asPart[ofPart++],
                /*city*/        asPart[ofPart++],
                /*state*/       asPart[ofPart++],
                /*zip*/         asPart[ofPart++],
                /*country*/     asPart[ofPart++]);
            Address addrWork       = new Address(
                /*streetline1*/ asPart[ofPart++],
                /*streetline2*/ asPart[ofPart++],
                /*city*/        asPart[ofPart++],
                /*state*/       asPart[ofPart++],
                /*zip*/         asPart[ofPart++],
                /*country*/     asPart[ofPart++]);
            IDictionary dictTelNum = new ListDictionary();

            for (int c = asPart.Length - 1; ofPart < c; )
            {
                dictTelNum.Add(/*type*/ asPart[ofPart++], new PhoneNumber(
                    /*access code*/  Convert.ToInt16(asPart[ofPart++]),
                    /*country code*/ Convert.ToInt16(asPart[ofPart++]),
                    /*area code*/    Convert.ToInt16(asPart[ofPart++]),
                    /*local num*/    Convert.ToInt64(asPart[ofPart++])));
            }
            
            return new Contact(sFirstName, sLastName, addrHome, addrWork, 
                dictTelNum, dtBirth);
        }

        #endregion

        #region Constants

        /// <summary>
        /// Default cache name.
        /// </summary>
        public const string CACHENAME = "contacts";

        /// <summary>
        /// The maximum number of contacts to load at a time.
        /// </summary>
        private const int BATCH_SIZE = 1;

        #endregion
    }
}
