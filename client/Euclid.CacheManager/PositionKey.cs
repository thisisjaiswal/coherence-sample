/*
 * #%L
 * PositionKey.cs
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
* PositionKey.cs
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

using Tangosol.IO.Pof;
using Tangosol.Util;

namespace Euclid.CacheManager.Pof
{
    /// <summary>
    /// PositionKey is a key to the person for whom information is tracked.
    /// </summary>
    /// <remarks>
    /// The type implements IPortableObject for efficient serialization.
    /// </remarks>
    /// <author>dag  2009.03.13</author>
    public class PositionKey : IPortableObject
    {
        #region Properties

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string PositionId
        {
            get { return m_positionId; }
        }

       

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public PositionKey()
        {
        }
        
        /// <summary>
        /// Construct a contact key.
        /// </summary>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        public PositionKey(string positionId)
        {
            m_positionId = positionId;
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_positionId = reader.ReadString(POSITION_ID);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteString(POSITION_ID, m_positionId);
        }

        #endregion

        #region Object override methods

        /// <summary>
        /// Equality based on the values of the <b>PositionKey</b> properties.
        /// </summary>
        /// <returns>
        /// A bool based on the equality of the <b>PositionKey</b> properties.
        /// </returns>
        public override bool Equals(object oThat)
        {
            if (oThat is PositionKey)
            {
                PositionKey that = (PositionKey) oThat;
                return Equals(PositionId, that.PositionId);
            }
            return false;
        }

        /// <summary>
        /// Return a string representation of this <b>ContactPerson</b>.
        /// </summary>
        /// <returns>
        /// A string representation of the <b>ContactPerson</b>.
        /// </returns>
        public override string ToString()
        {
            return PositionId;
        }

        /// <summary>
        /// Get a hash value for the <b>PositionKey</b> object.
        /// </summary>
        /// <returns>
        /// An integer hash value for this <b>PositionKey</b> object.
        /// </returns>
        public override int GetHashCode()
        {
            return (PositionId == null ? 0 : PositionId.GetHashCode());
        }

        #endregion

        #region Constants

        /// <summary>
        /// The POF index for the FirstName property
        /// </summary>
        public const int POSITION_ID = 0;
 

        #endregion

        #region Data members

        /// <summary>
        /// Position Id.
        /// </summary>
        private string m_positionId;

        
        
        #endregion
    }
}
