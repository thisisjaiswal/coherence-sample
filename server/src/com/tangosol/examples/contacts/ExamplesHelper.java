/*
 * #%L
 * ExamplesHelper.java
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

import com.oracle.common.base.Blocking;
import com.tangosol.examples.pof.Contact;
import com.tangosol.examples.pof.ContactId;
import com.tangosol.net.NamedCache;

/**
 * Various helper methods that are used by the examples.
 *
 * @author tam  2015.05.18
 * @since  12.2.1
 */
public class ExamplesHelper
    {
    // ----- static methods -------------------------------------------------

    /**
     * Log a message as a header.
     *
     * @param sMessage the message to log
     */
    public static void logHeader(String sMessage)
        {
        System.out.println("\n------" + sMessage + "------");
        System.out.flush();
        }

    /**
     * Log a message.
     *
     * @param sMessage the message to log
     */
    public static void log(String sMessage)
        {
        System.out.println("      " + sMessage);
        System.out.flush();
        }

    /**
     * Sleep a number of millis
     *
     * @param millisSleep the millis to sleep
     */
    public static void sleep(long millisSleep)
        {
        try
            {
            Blocking.sleep(millisSleep);
            }
        catch (InterruptedException e)
            {
            // ignore
            }
        }

    /**
     * Display a list of contacts first and last names.
     *
     * @param cache   the cache to display names from
     * @param sTitle  the title to display
     */
    public static void displayContactNames(NamedCache<ContactId, Contact> cache, String sTitle)
        {
        System.out.println("\nContacts " + sTitle);
        cache.forEach((k, v) -> System.out.println(
                v.getFirstName() + " " + v.getLastName()));
        }
    }
