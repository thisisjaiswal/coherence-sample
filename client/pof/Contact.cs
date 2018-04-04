/*
 * #%L
 * Contact.cs
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
* Contact.cs
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

namespace Tangosol.Examples.Pof
{
    /// <summary>
    /// Contact represents information needed to contact a person.
    /// </summary>
    /// <remarks>
    /// <p/>
    /// The type implements IPortableObject for efficient cross-platform 
    /// serialization.
    /// </remarks> 
    /// <author>dag  2009.02.17</author>
    public class Contact : IPortableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        /// <summary>
        /// Gets or sets the home address.
        /// </summary>
        /// <value>
        /// The home address.
        /// </value>
        public Address HomeAddress
        {
            get { return m_addrHome; }
            set { m_addrHome = value; }
        }

        /// <summary>
        /// Gets or sets the work address.
        /// </summary>
        /// <value>
        /// The work address.
        /// </value>
        public Address WorkAddress
        {
            get { return m_addrWork; }
            set { m_addrWork = value; }
        }

        /// <summary>
        /// Gets or sets all phone numbers.
        /// </summary>
        /// <value>
        /// A dictionary of phone numbers.
        /// </value>
        public IDictionary PhoneNumbers
        {
            get { return m_dictPhoneNumber; }
            set { m_dictPhoneNumber = value; }
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime BirthDate
        {
            get { return m_dtBirth; }
            set { m_dtBirth = value; }
        }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age
        {
            get { return m_nAge; }
            set { m_nAge = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public Contact()
        {
        }

        /// <summary>
        /// Construct Contact
        /// </summary>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName"> 
        /// The last name.
        /// </param>
        /// <param name="addrHome">
        /// The home address.
        /// </param>
        /// <param name="addrWork">
        /// The work address.
        /// </param>
        /// <param name="dictTelNumber">
        /// Map string number type (e.g. "work") to Phone.
        /// </param>
        /// <param name="dtBirth">
        /// Date of birth
        /// </param>
        public Contact(string firstName, string lastName, Address addrHome,
                Address addrWork, IDictionary dictTelNumber, DateTime dtBirth)
        {
            m_firstName       = firstName;
            m_lastName        = lastName;
            m_addrHome        = addrHome;
            m_addrWork        = addrWork;
            m_dictPhoneNumber = dictTelNumber;
            m_dtBirth         = dtBirth;
            CalculateAge();
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        void IPortableObject.ReadExternal(IPofReader reader)
        {
            m_firstName       = reader.ReadString(FIRST_NAME);
            m_lastName        = reader.ReadString(LAST_NAME);
            m_addrHome        = (Address)reader.ReadObject(HOME_ADDRESS);
            m_addrWork        = (Address) reader.ReadObject(WORK_ADDRESS);
            m_dictPhoneNumber = reader.ReadDictionary(PHONE_NUMBERS, 
                (IDictionary)new ListDictionary());
            m_dtBirth         = reader.ReadDate(BIRTH_DATE);
            m_nAge            = reader.ReadInt32(AGE);
        }

        /// <see cref="IPortableObject"/>
        void IPortableObject.WriteExternal(IPofWriter writer)
        {
            writer.WriteString(FIRST_NAME, m_firstName);
            writer.WriteString(LAST_NAME, m_lastName);
            writer.WriteObject(HOME_ADDRESS, m_addrHome);
            writer.WriteObject(WORK_ADDRESS, m_addrWork);
            writer.WriteDictionary(PHONE_NUMBERS, m_dictPhoneNumber,
                typeof(string), typeof(PhoneNumber));
            writer.WriteDate(BIRTH_DATE, m_dtBirth);
            writer.WriteInt32(AGE, m_nAge);
        }

        #endregion

        #region Object override methods

        /// <summary>
        /// A string representation of the values of the <b>Contact</b> object.
        /// </summary>
        /// <returns>
        /// A string representation of the Contact. 
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(FirstName)
                    .Append(" ")
                    .Append(LastName)
                    .Append("\nAddresses")
                    .Append("\nHome: ").Append(HomeAddress)
                    .Append("\nWork: ").Append(WorkAddress)
                    .Append("\nPhone Numbers");

            foreach (DictionaryEntry entry in m_dictPhoneNumber)
            {
                sb.Append("\n")
                  .Append(entry.Key).Append(": ").Append(entry.Value);
            }
            return (sb.Append("\nBirth Date: ").Append(BirthDate))
                      .Append(" (")
                      .Append(m_nAge)
                      .Append(" years old)")
                      .ToString();
        }

        #endregion

        #region helpers

        /// <summary>
        /// Calculate an age based upon the current birth date.
        /// </summary>
        public void CalculateAge()
        {
             m_nAge = (int) ((DateTime.Now - m_dtBirth).TotalDays / 365);
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

        /// <summary>
        /// The POF index for the HomeAddress property
        /// </summary>
        public const int HOME_ADDRESS = 2;

        /// <summary>
        /// The POF index for the WorkAddress property
        /// </summary>
        public const int WORK_ADDRESS = 3;

        /// <summary>
        /// The POF index for the PhoneNumbers property
        /// </summary>
        public const int PHONE_NUMBERS = 4;

        /// <summary>
        /// The POF index for the BirthDate property
        /// </summary>
        public const int BIRTH_DATE = 5;

        /// <summary>
        /// The POF index for the Age property
        /// </summary>
        public const int AGE = 6;

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

        /// <summary>
        /// Home address.
        /// </summary>
        private Address m_addrHome;

        /// <summary>
        /// Work address.
        /// </summary>
        private Address m_addrWork;

        /// <summary>
        /// Map of phone number type (such as "work", "home") to PhoneNumber.
        /// </summary>
        private IDictionary m_dictPhoneNumber;

        /// <summary>
        /// Birth date.
        /// </summary>
        private DateTime m_dtBirth;

        /// <summary>
        /// Age.
        // </summary>
        private int m_nAge;

        #endregion
        
        #region Constants

        /// <summary>
        /// Approximate number of millis in a year ignoring things such as leap
        /// years. Suitable for example use only.
        /// </summary>
        public const long MILLIS_IN_YEAR = 1000L * 60L * 60L * 24L * 365L;
        
        #endregion
    }
}
