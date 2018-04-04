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
package com.tangosol.examples.security;

import static com.tangosol.examples.contacts.ExamplesHelper.logHeader;

/**
 * Driver executes all the security examples.
 * <p>
 * Examples are invoked in this order <p/>
 * 1) Password enforcement<br/>
 * 2) Cache access control<br/>
 * 3) InvocationService access control<br/>
 *
 * @author dag  2010.04.15
 */
public class Driver
    {
    // ----- static methods -------------------------------------------------

    /**
     * Execute Security examples.
     * <p/>
     * usage: [cache-name]
     *
     * @param asArg command line arguments
     */
    public static void main(String[] asArg)
        throws Exception
        {
        logHeader("security examples begin");

        // Run password example
        PasswordExample.getCache();

        // Run cache access control examples
        AccessControlExample.shouldBeAbleToReadFromAndWriteToCache();

        AccessControlExample.shouldBeAbleToReadFromButNotWriteToCache();

        AccessControlExample.shouldBeAbleToReadFromButNotDestroyCache();

        AccessControlExample.shouldBeAbleToDestroyCacheWithAdminRights();

        // Run map listener access control example
        AccessControlExample.accessMapListener();

        // Run invocation service access control example
        AccessControlExample.shouldRunInvocablesWithReadWriteAccess();

        AccessControlExample.shouldNotRunInvocablesWithReadOnlyAccess();

        logHeader("security examples completed");
        }
    }
