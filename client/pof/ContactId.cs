/*
 * #%L
 * ContactId.cs
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
* ContactId.cs
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
    /// ContactId is a key to the person for whom information is tracked.
    /// </summary>
    /// <remarks>
    /// The type implements IPortableObject for efficient serialization.
    /// </remarks>
    /// <author>dag  2009.03.13</author>
    public class ContactId : IPortableObject
    {
        #region Properties

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get { return m_firstName; }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get { return m_lastName; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public ContactId()
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
        public ContactId(string firstName, string lastName)
        {
            m_firstName = firstName;
            m_lastName  = lastName;
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_firstName = reader.ReadString(FIRST_NAME);
            m_lastName  = reader.ReadString(LAST_NAME);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteString(FIRST_NAME, m_firstName);
            writer.WriteString(LAST_NAME, m_lastName);
        }

        #endregion

        #region Object override methods

        /// <summary>
        /// Equality based on the values of the <b>ContactId</b> properties.
        /// </summary>
        /// <returns>
        /// A bool based on the equality of the <b>ContactId</b> properties.
        /// </returns>
        public override bool Equals(object oThat)
        {
            if (oThat is ContactId)
            {
                ContactId that = (ContactId) oThat;
                return Equals(FirstName, that.FirstName) &&
                       Equals(LastName,  that.LastName);
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
            return FirstName + " " + LastName;
        }

        /// <summary>
        /// Get a hash value for the <b>ContactId</b> object.
        /// </summary>
        /// <returns>
        /// An integer hash value for this <b>ContactId</b> object.
        /// </returns>
        public override int GetHashCode()
        {
            return (FirstName == null ? 0 : FirstName.GetHashCode()) ^
                   (LastName == null ? 0 : LastName.GetHashCode());
        }

        #endregion

        #region Constants

        /// <summary>
        /// The POF index for the FirstName property
        /// </summary>
        public const int FIRST_NAME = 0;

        /// <summary>
        /// The POF index for the LastName property
        /// </summary>
        public const int LAST_NAME = 1;

        #endregion

        #region Data members

        /// <summary>
        /// First name.
        /// </summary>
        private string m_firstName;

        /// <summary>
        /// Last name.
        /// </summary>
        private string m_lastName;
        
        #endregion
    }
}
