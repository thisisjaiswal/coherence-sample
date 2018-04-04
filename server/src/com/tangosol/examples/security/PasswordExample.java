/*
 * #%L
 * PasswordExample.java
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

import com.tangosol.net.NamedCache;
import com.tangosol.net.Session;
import com.tangosol.net.cache.TypeAssertion;

import java.security.PrivilegedExceptionAction;

import javax.security.auth.Subject;

import static com.tangosol.examples.contacts.ExamplesHelper.log;
import static com.tangosol.examples.contacts.ExamplesHelper.logHeader;

/**
 * This class shows how a Coherence Proxy can require a password to get a
 * reference to a cache.
 * <p>
 * The PasswordIdentityTransformer will generate a security token that
 * contains the password. The PasswordIdentityAsserter will validate the
 * security token to enforce the password. The token generation and
 * validation occurs automatically when a connection to the proxy is made.
 *
 * @author dag 2010.04.16
 */
@SuppressWarnings (value="unchecked")
public class PasswordExample
    {
    // ----- static methods -------------------------------------------------

    /**
     * Get a reference to the cache. Password will be required.
     */
    public static void getCache()
        {
        logHeader("password example begins");

        Subject subject = SecurityExampleHelper.login("BuckarooBanzai");

        try
            {
            NamedCache cache = (NamedCache) Subject.doAs(
                    subject, (PrivilegedExceptionAction) () ->
                {
                NamedCache cache1;
                Session session = Session.create();

                cache1 = session.getCache(SecurityExampleHelper.SECURITY_CACHE_NAME,
                  TypeAssertion.withoutTypeChecking());
                logHeader("password example succeeded");
                return cache1;
                });
            }
        catch (Exception e)
            {
            // get exception if the password is invalid
            log("Unable to connect to proxy");
            e.printStackTrace();
            }
        logHeader("password example completed");
        }
    }
