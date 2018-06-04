/*
 * #%L
 * Contact.java
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

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;
import java.io.IOException;

import java.time.LocalDate;
import java.time.Period;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/**
 * Position represents information needed to contact a person.
 * <p/>
 * The type implements PortableObject for efficient cross-platform
 * serialization..
 *
 * @author dag  2009.02.17
 */
@XmlRootElement(name="position")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class Position
        implements PortableObject
    {
    // ----- constructors ---------------------------------------------------

    /**
     * Default constructor (necessary for PortableObject implementation).
     */
    public Position()
        {
        }


    // ----- accessors ------------------------------------------------------


        public String getPositionid()
            {
            return m_sPositionId;
            }
        public void setPositionid(String sPositionId)
            {
            m_sPositionId = sPositionId;
            }

        public String getField1()
        {
            return m_sField1;
        }
        public void setField1(String sField1)
        {
            m_sField1 = sField1;
        }

        public String getField2()
        {
            return m_sField2;
        }
        public void setField2(String sField2)
        {
            m_sField2 = sField2;
        }

        public String getField3()
        {
            return m_sField3;
        }
        public void setField3(String sField3)
        {
            m_sField3 = sField3;
        }

        public String getField4()
        {
            return m_sField4;
        }
        public void setField4(String sField4)
        {
            m_sField4 = sField4;
        }

        public String getField5()
        {
            return m_sField5;
        }
        public void setField5(String sField5)
        {
            m_sField5 = sField5;
        }

        public String getField6()
        {
            return m_sField6;
        }
        public void setField6(String sField6)
        {
            m_sField6 = sField6;
        }

        public String getField7()
        {
            return m_sField7;
        }
        public void setField7(String sField7)
        {
            m_sField7 = sField7;
        }

        public String getField8()
        {
            return m_sField8;
        }
        public void setField8(String sField8)
        {
            m_sField8 = sField8;
        }

        public String getField9()
        {
            return m_sField9;
        }
        public void setField9(String sField9)
        {
            m_sField9 = sField9;
        }


    /**
     * {@inheritDoc}
     */
    public void readExternal(PofReader reader)
            throws IOException
        {
        m_sPositionId     = reader.readString(POSITIONID);
        m_sField1 = reader.readString(FIELD1);
            m_sField2 = reader.readString(FIELD2);
            m_sField3 = reader.readString(FIELD3);
            m_sField4 = reader.readString(FIELD4);
            m_sField5 = reader.readString(FIELD5);
            m_sField6 = reader.readString(FIELD6);
            m_sField7 = reader.readString(FIELD7);
            m_sField8 = reader.readString(FIELD8);
            m_sField9 = reader.readString(FIELD9);
        }

    /**
     * {@inheritDoc}
     */
    public void writeExternal(PofWriter writer)
            throws IOException
        {
        writer.writeString(POSITIONID, m_sPositionId);
            writer.writeString(FIELD1, m_sField1);
            writer.writeString(FIELD2, m_sField2);
            writer.writeString(FIELD3, m_sField3);
            writer.writeString(FIELD4, m_sField4);
            writer.writeString(FIELD5, m_sField5);
            writer.writeString(FIELD6, m_sField6);
            writer.writeString(FIELD7, m_sField7);
            writer.writeString(FIELD8, m_sField8);
            writer.writeString(FIELD9, m_sField9);
        }

    // ----- Object methods -------------------------------------------------



    // ----- helpers ---------------------------------------------------------



    // ----- constants -------------------------------------------------------

    /**
     * The POF index for the FirstName property.
     */
    public static final int POSITIONID = 0;

    /**
     * The POF index for the LastName property.
     */
    public static final int FIELD1 = 1;

    /**
     * The POF index for the HomeAddress property.
     */
    public static final int FIELD2 = 2;

    /**
     * The POF index for the WorkAddress property.
     */
    public static final int FIELD3 = 3;

    /**
     * The POF index for the PhoneNumbers property.
     */
    public static final int FIELD4 = 4;

    /**
     * The POF index for the BirthDate property.
     */
    public static final int FIELD5 = 5;

    /**
     * The POF index for the age property.
     */
    public static final int FIELD6 = 6;

        public static final int FIELD7 = 7;

        public static final int FIELD8 = 8;

        public static final int FIELD9 = 9;

    // ----- data members ---------------------------------------------------

        private String m_sPositionId;
        private String m_sField1;
        private String m_sField2;
        private String m_sField3;
        private String m_sField4;
        private String m_sField5;
        private String m_sField6;
        private String m_sField7;
        private String m_sField8;
        private String m_sField9;

    }
