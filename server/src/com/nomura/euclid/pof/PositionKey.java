/*
 * #%L
 * ContactId.java
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
package com.nomura.euclid.pof;

import com.tangosol.io.pof.PofReader;
import com.tangosol.io.pof.PofWriter;
import com.tangosol.io.pof.PortableObject;

import com.tangosol.util.Base;

import java.io.IOException;

/**
 * PositionKey represents a key to the contact for whom information is stored in
 * the cache.
 * <p/>
 * The type implements PortableObject for efficient cross-platform
 * serialization..
 *
 * @author dag  2009.02.18
 */
public class PositionKey
        implements PortableObject
    {
    // ----- constructors ---------------------------------------------------

    /**
     * Default constructor (necessary for PortableObject implementation).
     */
    public PositionKey()
        {
        }


    public PositionKey(String sPositionId)
        {
        m_sPositionId = sPositionId;
        }

    // ----- accessors ------------------------------------------------------

    /**
     * Return the first name.
     *
     * @return the first name
     */
    public String getPositionId()
        {
        return m_sPositionId;
        }



    // ----- PortableObject interface ---------------------------------------

    /**
     * {@inheritDoc}
     */
    public void readExternal(PofReader reader)
            throws IOException
        {
        m_sPositionId = reader.readString(POSITIONID);
        }

    /**
     * {@inheritDoc}
     */
    public void writeExternal(PofWriter writer)
            throws IOException
        {
        writer.writeString(POSITIONID, m_sPositionId);
        }

    // ----- Object methods -------------------------------------------------

    /**
     * {@inheritDoc}
     */
    public boolean equals(Object oThat)
        {
        if (this == oThat)
            {
            return true;
            }
        if (oThat == null)
            {
            return false;
            }

        PositionKey that = (PositionKey) oThat;
        return Base.equals(getPositionId(), that.getPositionId());
        }

    /**
     * {@inheritDoc}
     */
    public int hashCode()
        {
        return (getPositionId() == null ? 0 : getPositionId().hashCode());

        }

    /**
     * {@inheritDoc}
     */
    public String toString()
        {
        return getPositionId();
        }

    // ----- constants -------------------------------------------------------

    /**
     * The POF index for the FirstName property.
     */
    public static final int POSITIONID = 0;



    // ----- data members ---------------------------------------------------


    private String m_sPositionId;



    }
