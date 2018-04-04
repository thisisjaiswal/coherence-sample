/*
 * #%L
 * OfficeUpdater.cs
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

using System;
using System.Text;

using Tangosol.Examples.Pof;
using Tangosol.IO.Pof;
using Tangosol.Net;
using Tangosol.Net.Cache;
using Tangosol.Util.Processor;

namespace Tangosol.Examples.Pof
{
    /// <summary>
    /// OfficeUpdater updates a contact's office address.
    /// </summary>
    /// <remarks>
    /// The implementations in this class will not be called for remote
    /// caches; instead, the equivalent Java class will execute in the remote
    /// cache. When using a local cache in .NET, these implementations will be
    /// used. Note, that state will be serialized and passed to the remote
    /// cache.
    /// </remarks>
    /// <author>dag  2009.03.26</author>
    public class OfficeUpdater : AbstractProcessor, IPortableObject
    {
        #region Constructors

        /// <summary>
        /// Default constructor (necessary for IPortableObject implementation).
        /// </summary>
        public OfficeUpdater()
        {
        }

        /// <summary>
        /// Construct an OfficeUpdater with a new work Address.
        /// </summary>
        /// <param name="addrWork">
        /// The new work address.
        /// </param>
        public OfficeUpdater(Address addrWork)
        {
            m_addrWork = addrWork;
        }

        #endregion

        #region AbstractProcessor implementation

        /// <see cref="AbstractProcessor"/>
        public override object Process(IInvocableCacheEntry entry)
        {
            Contact contact = (Contact)entry.Value;

            contact.WorkAddress = m_addrWork;
            entry.SetValue(contact, false);
            return null;
        }

        #endregion

        #region IPortableObject implementation

        /// <see cref="IPortableObject"/>
        public void ReadExternal(IPofReader reader)
        {
            m_addrWork = (Address)reader.ReadObject(WORK_ADDRESS);
        }

        /// <see cref="IPortableObject"/>
        public void WriteExternal(IPofWriter writer)
        {
            writer.WriteObject(WORK_ADDRESS, m_addrWork);
        }

        #endregion
        
        #region Constants

        /// <summary>
        /// The POF index for the WorkAddress property
        /// </summary>
        public const int WORK_ADDRESS = 0;

        #endregion

        #region Data members

        /// <summary>
        /// New work address.
        /// </summary>
        private Address m_addrWork;

        #endregion
    }
}
