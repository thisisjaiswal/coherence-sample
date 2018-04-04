/*
 * #%L
 * Driver.java
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
package com.tangosol.examples.contacts;

import com.tangosol.examples.pof.Contact;
import com.tangosol.examples.pof.ContactId;
import com.tangosol.net.NamedCache;
import com.tangosol.net.Session;
import com.tangosol.util.Resources;

import java.io.IOException;

import java.net.URL;

import static com.tangosol.examples.contacts.ExamplesHelper.logHeader;
import static com.tangosol.net.cache.TypeAssertion.withTypes;

/**
 * Driver executes all the contact examples. The driver will if not passed a
 * cache name use the 'contacts' as as the default cache name. If
 * passed a different name, make sure that the changes are reflected in the
 * configuration files.
 * <p/>
 * Before the examples are run, the Driver will populate the cache with random
 * contact data.
 * <p/>
 * Examples are invoked in this order <p/>
 * 1) LoaderExample<br/>
 * 2) QueryExample <br/>
 * 3) QueryLanguageExample <br/>
 * 4) ObserverExample <br/>
 * 5) BasicExample<br/>
 * 6) ProcessorExample<br/>
 *
 * @author dag  2009.03.02
 */
public class Driver
    {
    // ----- static methods -------------------------------------------------

    /**
     * Execute Contact examples.
     * <p/>
     * usage: [cache-name] [contacts file]
     *
     * @param asArgs command line arguments
     */
    public static void main(String[] asArgs)
            throws IOException
        {
        String sCache = asArgs.length > 0 ? asArgs[0] :
                LoaderExample.CACHENAME;
        String sFile = asArgs.length > 1 ? asArgs[1] + "/../resource/" +
                DEFAULT_DATAFILE : DEFAULT_DATAFILE;

        // Session implements AutoClosable
        try (Session session = Session.create())
            {
            NamedCache<ContactId, Contact> cache = session.getCache(sCache,
                     withTypes(ContactId.class, Contact.class));

            logHeader("contacts examples begin");
            // Load data into cache
            URL contactFileURL = Resources.findFileOrResource(sFile, Driver.class.getClassLoader());
            new LoaderExample().load(contactFileURL.openStream(), cache);

            // Run sample queries
            new QueryExample().query(cache);

            // Run sample queries using query language
            new QueryLanguageExample().query(cache, new FilterFactory("InvocationService"));

            // Run sample change observer.
            ObserverExample observer = new ObserverExample();
            observer.observe(cache);

            // Run basic cache commands
            new BasicExample().execute(cache);

            // Run sample entry processor
            new ProcessorExample().execute(cache);

            // Stop observing
            observer.remove(cache);

            logHeader("contacts examples completed");
            }
        catch (Exception e)
            {
            logHeader("contacts examples completed with error");
            e.printStackTrace();
            }

        }

    // ----- constants ------------------------------------------------------

    public final static String DEFAULT_DATAFILE = "contacts.csv";
    }
