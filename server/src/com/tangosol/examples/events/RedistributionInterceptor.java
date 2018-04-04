/*
 * #%L
 * RedistributionInterceptor.java
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
package com.tangosol.examples.events;

import com.tangosol.examples.pof.RedistributionInvocable;

import com.tangosol.net.CacheFactory;

import com.tangosol.net.events.EventInterceptor;
import com.tangosol.net.events.annotation.Interceptor;
import com.tangosol.net.events.partition.TransferEvent;

/**
 * RedistributionInterceptor is an {@link com.tangosol.net.events.EventInterceptor}
 * that logs partition activity when enabled. Logging can be enabled via
 * setting the {@link RedistributionInvocable#ENABLED} constant.
 *
 * @author hr  2011.11.30
 * @since Coherence 12.1.2
 */
@Interceptor(identifier = "redist")
public class RedistributionInterceptor
        implements EventInterceptor<TransferEvent>
    {

    // ----- EventInterceptor methods ---------------------------------------

    /**
     * {@inheritDoc}
     */
    public void onEvent(TransferEvent event)
        {
        if (RedistributionInvocable.ENABLED.get())
            {
            logEvent(event);
            }
        }

    protected void logEvent(TransferEvent event)
        {
        CacheFactory.log(String.format("Discovered event %s for partition-id %d from remote member %s\n",
            event.getType(), event.getPartitionId(), event.getRemoteMember()),
            CacheFactory.LOG_INFO);
        }
    }
