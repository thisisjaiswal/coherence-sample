/*
 * #%L
 * LazyProcessor.java
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
package com.tangosol.examples.pof;

import com.tangosol.io.pof.PofReader;
import com.tangosol.io.pof.PofWriter;
import com.tangosol.io.pof.PortableObject;
import com.tangosol.util.Base;
import com.tangosol.util.InvocableMap.Entry;
import com.tangosol.util.processor.AbstractProcessor;

import java.io.IOException;

/**
 * LazyProcessor will sleep for a specified period of time.
 *
 * @author hr  2011.11.30
 *
 * @since 12.1.2
 */
public class LazyProcessor
        extends AbstractProcessor
        implements PortableObject
    {
    // ----- constructors ---------------------------------------------------

    /**
     * Default no-arg constructor.
     */
    public LazyProcessor()
        {
        }

    /**
     * Constructs a LazyProcessor with a specified time to sleep.
     *
     * @param lSleepTime  the number of milliseconds this processor should
     *                    sleep
     */
    public LazyProcessor(long lSleepTime)
        {
        m_lSleepTime = lSleepTime;
        }

    /**
     * {@inheritDoc}
     */
    public Object process(Entry entry)
        {
        try
            {
            Thread.sleep(m_lSleepTime);
            }
        catch (InterruptedException e)
            {
            throw Base.ensureRuntimeException(e);
            }
        return null;
        }

    // ----- PortableObject members -----------------------------------------

    /**
     * {@inheritDoc}
     */
    public void readExternal(PofReader in) throws IOException
        {
        m_lSleepTime = in.readLong(0);
        }

    /**
     * {@inheritDoc}
     */
    public void writeExternal(PofWriter out) throws IOException
        {
        out.writeLong(0, m_lSleepTime);
        }

    // ----- data members ---------------------------------------------------

    /**
     * The number of milliseconds this processor should sleep.
     */
    protected long m_lSleepTime;
    }
