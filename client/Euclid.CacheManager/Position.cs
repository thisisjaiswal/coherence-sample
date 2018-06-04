/*
 * #%L
 * Position.cs
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
/*
* Position.cs
*
* Copyright (c) 2000, 2017, Oracle and/or its affiliates. All rights reserved.
*
* Oracle is a registered trademarks of Oracle Corporation and/or its
* affiliates.
*
* This software is the confidential and proprietary information of Oracle
* Corporation. You shall not disclose such confidential and proprietary
* information and shall use it only in accordance with the terms of the
* license agreement you entered into with Oracle.
*
* This notice may not be removed or altered.
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

using Tangosol.IO.Pof;
using Tangosol.Util;

namespace Euclid.CacheManager.Pof
{
    /// <summary>
    /// Position represents information needed to contact a person.
    /// </summary>
    /// <remarks>
    /// <p/>
    /// The type implements IPortableObject for efficient cross-platform 
    /// serialization.
    /// </remarks> 
    /// <author>dag  2009.02.17</author>
    public class Position : IPortableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string PositionId
        {
            get { return m_positionId; }
            set { m_positionId = value; }
        }

        public string Field1
        {
            get { return m_field1; }
            set { m_field1 = value; }
        }

        public string Field2
        {
            get { return m_field2; }
            set { m_field2 = value; }
        }

        public string Field3
        {
            get { return m_field3; }
            set { m_field3 = value; }
        }      

        public string Field4
        {
            get { return m_field4; }
            set { m_field4 = value; }
        }

        public string Field5
        {
            get { return m_field5; }
            set { m_field5 = value; }
        }

        public string Field6
        {
            get { return m_field6; }
            set { m_field6 = value; }
        }

        public string Field7
        {
            get { return m_field7; }
            set { m_field7 = value; }
        }

        public string Field8
        {
            get { return m_field8; }
            set { m_field8 = value; }
        }

        public string Field9
        {
            get { return m_field9; }
            set { m_field9 = value; }
        }






        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public Position()
        {
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_positionId      = reader.ReadString(POSITION_ID);
            m_field1 = reader.ReadString(FIELD_1);
            m_field2 = reader.ReadString(FIELD_2);
            m_field3 = reader.ReadString(FIELD_3);
            m_field4 = reader.ReadString(FIELD_4);
            m_field5 = reader.ReadString(FIELD_5);
            m_field6 = reader.ReadString(FIELD_6);
            m_field7 = reader.ReadString(FIELD_7);
            m_field8 = reader.ReadString(FIELD_8);
            m_field9 = reader.ReadString(FIELD_9);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteString(POSITION_ID, m_positionId);
            writer.WriteString(FIELD_1, m_field1);
            writer.WriteString(FIELD_2, m_field2);
            writer.WriteString(FIELD_3, m_field3);
            writer.WriteString(FIELD_4, m_field4);
            writer.WriteString(FIELD_5, m_field5);
            writer.WriteString(FIELD_6, m_field6);
            writer.WriteString(FIELD_7, m_field7);
            writer.WriteString(FIELD_8, m_field8);
            writer.WriteString(FIELD_9, m_field9);
        }

        #endregion
 

       

        #region Constants

        
        public const int POSITION_ID = 0; 
        public const int FIELD_1 = 1;
        public const int FIELD_2 = 2;
        public const int FIELD_3 = 3;
        public const int FIELD_4 = 4;
        public const int FIELD_5 = 5;
        public const int FIELD_6 = 6;
        public const int FIELD_7 = 7;
        public const int FIELD_8 = 8;
        public const int FIELD_9 = 9;


        #endregion

        #region Data members

        /// <summary>
        /// First name.
        /// </summary>
        private string m_positionId;       
        private string m_field1;
        private string m_field2;
        private string m_field3;
        private string m_field4;
        private string m_field5;
        private string m_field6;
        private string m_field7;
        private string m_field8;
        private string m_field9;

        #endregion

        
    }
}
