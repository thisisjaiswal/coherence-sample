/*
 * #%L
 * Address.cs
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
* Address.cs
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
    /// Address is a place where a contact is located.
    /// </summary>
    /// <remarks>
    /// <p/>
    /// The type implements IPortableObject for efficient cross-platform 
    /// serialization.
    /// </remarks>
    /// <author>dag  2009.03.12</author>
    public class Address : IPortableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets the first line of the street address.
        /// </summary>
        /// <value>
        /// The first line of the street address.
        /// </value>
        public string Street1
        {
            get { return m_street1; }
            set { m_street1 = value; }
        }

        /// <summary>
        /// Gets or sets the second line of the street address.
        /// </summary>
        /// <value>
        /// The second line of the street address.
        /// </value>
        public string Street2
        {
            get { return m_street2; }
            set { m_street2 = value; }
        }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }

        /// <summary>
        /// Gets or sets the state name.
        /// </summary>
        /// <value>
        /// The state name
        /// </value>
        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode
        {
            get { return m_zip; }
            set { m_zip = value; }
        }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        /// <value>
        /// The country name.
        /// </value>
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Construct an Address.
        /// </summary>
        /// <param name="street1">
        /// The first line of the street address.
        /// </param>
        /// <param name="street2">
        /// The second line of the street address.
        /// </param>
        /// <param name="city">
        /// The city name.
        /// </param>
        /// <param name="state">
        /// The state name.
        /// </param>
        /// <param name="zip">
        /// The zip (postal) code.
        /// </param>
        /// <param name="country">
        /// The country name.
        /// </param>
        public Address(string street1, string street2, string city,
            string state, string zip, string country)
        {
            m_street1 = street1;
            m_street2 = street2;
            m_city    = city;
            m_state   = state;
            m_zip     = zip;
            m_country = country;
        }
        
        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_street1 = reader.ReadString(STREET_1);
            m_street2 = reader.ReadString(STREET_2);
            m_city    = reader.ReadString(CITY);
            m_state   = reader.ReadString(STATE);
            m_zip     = reader.ReadString(ZIP);
            m_country = reader.ReadString(COUNTRY);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteString(STREET_1, m_street1);
            writer.WriteString(STREET_2, m_street2);
            writer.WriteString(CITY, m_city);
            writer.WriteString(STATE, m_state);
            writer.WriteString(ZIP, m_zip);
            writer.WriteString(COUNTRY, m_country);
        }

        #endregion
        
        #region Object override methods

        /// <summary>
        /// Equality based on the values of the <b>Address</b> properties.
        /// </summary>
        /// <returns>
        /// A bool based on the equality of the <b>Address</b> properties.
        /// </returns>
        public override bool Equals(object oThat)
        {
            if (oThat is Address)
            {
                Address that = (Address) oThat;
                return Equals(Street1, that.Street1) &&
                       Equals(Street2, that.Street2) &&
                       Equals(City,    that.City)    &&
                       Equals(ZipCode, that.ZipCode) &&
                       Equals(Country, that.Country);
            }
            return false;
        }

        /// <summary>
        /// Return a string representation of this <b>Address</b>.
        /// </summary>
        /// <returns>
        /// A string representation of the address.
        /// </returns>
        public override string ToString()
        {
            return Street1 + "\n" +
                   Street2 + "\n" +
                   City + ", " + State + " " + ZipCode + "\n" +
                   Country;
        }

        /// <summary>
        /// Get a hash value for the <b>Address</b> object.
        /// </summary>
        /// <returns>
        /// An integer hash value for this <b>Address</b> object.
        /// </returns>
        public override int GetHashCode()
        {
            return (Street1 == null ? 0 : Street1.GetHashCode()) ^
                   (Street2 == null ? 0 : Street2.GetHashCode()) ^
                   (ZipCode == null ? 0 : ZipCode.GetHashCode());
        }

        #endregion

        #region Constants
            
        /// <summary>
        /// The POF index for the Street1 property
        /// </summary>
        public const int STREET_1 = 0;

        /// <summary>
        /// The POF index for the Street2 property
        /// </summary>
        public const int STREET_2 = 1;

        /// <summary>
        /// The POF index for the City property
        /// </summary>
        public const int CITY = 2;

        /// <summary>
        /// The POF index for the State property
        /// </summary>
        public const int STATE = 3;

        /// <summary>
        /// The POF index for the Zip property
        /// </summary>
        public const int ZIP = 4;

        /// <summary>
        /// The POF index for the Country property
        /// </summary>
        public const int COUNTRY = 5;
        
        #endregion
        
        #region Data members

        /// <summary>
        /// First line of street address.
        /// </summary>
        private string m_street1;

        /// <summary>
        /// Second line of street address.
        /// </summary>
        private string m_street2;

        /// <summary>
        /// City.
        /// </summary>
        private string m_city;

        /// <summary>
        /// State or Province.
        /// </summary>
        private string m_state;

        /// <summary>
        /// Zip or other postal code.
        /// </summary>
        private string m_zip;

        /// <summary>
        /// Country.
        /// </summary>
        private string m_country;

    #endregion
    }
}
