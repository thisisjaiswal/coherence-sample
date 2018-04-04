/*
 * #%L
 * PhoneNumber.cs
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
* PhoneNumber.cs
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

namespace Tangosol.Examples.Pof
{
    /// <summary>
    /// Phone represents a sequence of numbers used to call a telephone.
    /// </summary>
    /// <remarks>
    /// An example that uses the full sequence of numbers is a call from the 
    /// United States to Beijing, China: 011 86 10 85001234.
    /// <p/>
    /// The type implements IPortableObject for efficient cross-platform 
    /// serialization.
    /// </remarks>
    /// <author>dag  2009.03.13</author>
    public class PhoneNumber : IPortableObject
    {        
        #region Properties

        /// <summary>
        /// Gets or sets the access code.
        /// </summary>
        /// <value>
        /// The access code.
        /// </value>
        public short AccessCode
        {
            get { return m_accessCode; }
            set { m_accessCode = value; }
        }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public short CountryCode
        {
            get { return m_countryCode; }
            set { m_countryCode = value; }
        }

        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        /// <value>
        /// The area code.
        /// </value>
        public short AreaCode
        {
            get { return m_areaCode; }
            set { m_areaCode = value; }
        }

        /// <summary>
        /// Gets or sets the local number.
        /// </summary>
        /// <value>
        /// The local number.
        /// </value>
        public long LocalNumber
        {
            get { return m_localNumber; }
            set { m_localNumber = value; }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public PhoneNumber()
        {
        }

        /// <summary>
        /// Construct a PhoneNumber.
        /// </summary>
        /// <param name="accessCode">
        /// The numbers used to access international or non-local calls.
        /// </param>
        /// <param name="countryCode">
        /// The numbers used to designate a country.
        /// </param>
        /// <param name="areaCode">
        /// The numbers used to indicate a geographical region.
        /// </param>
        /// <param name="localNumber">
        /// The local numbers.
        /// </param>
        public PhoneNumber(short accessCode, short countryCode,
            short areaCode, long localNumber)
        {
            m_accessCode = accessCode;
            m_countryCode = countryCode;
            m_areaCode = areaCode;
            m_localNumber = localNumber;
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_accessCode  = reader.ReadInt16(0);
            m_countryCode = reader.ReadInt16(1);
            m_areaCode    = reader.ReadInt16(2);
            m_localNumber = reader.ReadInt64(3);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteInt16(0, m_accessCode);
            writer.WriteInt16(1, m_countryCode);
            writer.WriteInt16(2, m_areaCode);
            writer.WriteInt64(3, m_localNumber);
        }

        #endregion

        #region Object override methods

        /// <summary>
        /// Equality based on the values of the <b>Phone</b> properties.
        /// </summary>
        /// <returns>
        /// A bool based on the equality of the <b>Phone</b> properties.
        /// </returns>
        public override bool Equals(System.Object oThat)
        {
            if (oThat is PhoneNumber)
            {
                PhoneNumber that = (PhoneNumber)oThat; 
                return AccessCode  == that.AccessCode  &&
                       CountryCode == that.CountryCode &&
                       AreaCode    == that.AreaCode    &&
                       LocalNumber == that.LocalNumber;
            }
            return false;
        }

        /// <summary>
        /// Return a string representation of this <b>Phone</b>.
        /// </summary>
        /// <returns>
        /// A string representation of the Phone.
        /// </returns>
        public override string ToString()
        {
            return "+" + AccessCode + " " + CountryCode + " "
                       + AreaCode   + " " + LocalNumber;
        }

        /// <summary>
        /// Get a hash value for the <b>Phone</b> object.
        /// </summary>
        /// <returns>
        /// An integer hash value for this <b>Phone</b> object.
        /// </returns>
        public override int GetHashCode()
        {
            return (int) ((long) AreaCode * 31L + LocalNumber);
        }

        #endregion
        
        #region Data members

        /// <summary>
        /// The numbers used to designate a country in international calls.
        /// </summary>
        private short m_accessCode;

        /// <summary>
        /// The numbers used indicate a geographic area within a country.
        /// </summary>
        private short m_countryCode;

        /// <summary>
        /// The numbers used indicate a geographic area within a country.
        /// </summary>
        private short m_areaCode;

        /// <summary>
        /// the local number
        /// </summary>
        private long m_localNumber;

        #endregion
    }
}
