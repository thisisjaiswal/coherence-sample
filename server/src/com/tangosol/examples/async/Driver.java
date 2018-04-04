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
package com.tangosol.examples.async;

import com.tangosol.examples.contacts.DataGenerator;
import com.tangosol.examples.pof.Contact;
import com.tangosol.examples.pof.ContactId;
import com.tangosol.net.NamedCache;
import com.tangosol.net.Session;

import static com.tangosol.examples.contacts.ExamplesHelper.log;
import static com.tangosol.examples.contacts.ExamplesHelper.logHeader;
import static com.tangosol.net.cache.TypeAssertion.withTypes;

/**
 * Driver executes all the Coherence Async examples.
 *
 * @author tam  2015.05.22
 * @since 12.2.1
 */
public class Driver
    {
    // ----- static methods -------------------------------------------------

    public static void main (String[] asArgs)
        {
        try(Session session = Session.create())
            {
            NamedCache<ContactId, Contact> cache =
                  session.getCache("contacts", withTypes(ContactId.class, Contact.class));

            new Driver().runExamples(cache);
            }
        catch (Exception e)
            {
            log("Error running async example");
            e.printStackTrace();
            }
        }

    // ----- Driver methods -------------------------------------------------

    /**
     * Run the Java8 examples.
     *
     * @param cache  the cache to use
     */
    public void runExamples(NamedCache<ContactId, Contact> cache)
        {
        logHeader("Async examples begin");

        cache.clear();
        cache.putAll(DataGenerator.generateContacts(100));

        try
            {
            new DataAccessExample().runExample(cache);

            new ProcessorExample().runExample(cache);

            new AggregatorExample().runExample(cache);
            }
        catch (Exception e)
            {
            System.out.println("Example completed with an error. " + e.getMessage());
            e.printStackTrace();
            }

        logHeader("Async examples completed");
        }
    }
