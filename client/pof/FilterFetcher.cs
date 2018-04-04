/*
 * #%L
 * FilterFetcher.cs
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
﻿
using System.Collections;
using System.IO;
using Tangosol.IO.Pof;
using Tangosol.Net;

namespace Tangosol.Examples.Pof
{
    /// <summary>
    /// FilterFetcher is a class that supports getting <b>IFilters</b> or
    /// <b>IValueExtractors</b> by using an <b>IInvocationService</b>.
    /// </summary>
    /// <author>Ivan Cikic  2010.03.09</author>
    public class FilterFetcher : AbstractInvocable
    {
        #region Constructors

        /// <summary>
        /// Construct a new FilterFetcher.
        /// </summary>
        public FilterFetcher() 
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Construct a new FilterFetcher that will return an IFilter based on
        /// the given string.
        /// </summary>
        /// <param name="query">
        ///  The string that defines the IFilter.
        /// </param>
        public FilterFetcher(string query) 
            : this(query, null, null)
        {
        }

        /// <summary>
        /// Construct a new FilterFetcher that will return an IFilter based on
        /// the given string. The given flag controls whether an IFilter vs.
        /// an IValueExtractor is retreived.
        /// </summary>
        /// <param name="query">
        /// The string that defines the IFilter.
        /// </param>
        /// <param name="fetchExtractor">
        /// A flag that controles the type of value to retrieve.
        /// </param>
        public FilterFetcher(string query, bool fetchExtractor)
        {
            m_query          = query;
            m_fetchExtractor = fetchExtractor;
        }

        /// <summary>
        /// Construct a new FilterFetcher that will return an IFilter based on
        /// the given string and binding environment.
        /// </summary>
        /// <param name="query">
        /// The string that defines the IFilter.
        /// </param>
        /// <param name="env">
        /// An object[] that specifies the binding environment.
        /// </param>
        public FilterFetcher(string query, object[] env)
            : this(query, env, null)
        {
        }

        /// <summary>
        /// Construct a new FilterFetcher that will return an IFilter based on
        /// the given string and binding environment.
        /// </summary>
        /// <param name="query">The string that defines the IFilter.</param>
        /// <param name="bindings">
        /// A dictionary that specifies the binding environment.
        /// </param>
        public FilterFetcher(string query, IDictionary bindings)
            : this(query, null, bindings)
        {
        }

        /// <summary>
        /// Construct a new FilterFetcher that will return an IFilter based on
        /// the given string and binding environments.
        /// </summary>
        /// <param name="query">
        /// The string that defines the IFilter.
        /// </param>
        /// <param name="env">
        /// An object[] that specifies the binding environment.
        /// </param>
        /// <param name="bindings">
        /// A dictionary that specifies the binding environment.
        /// </param>
        public FilterFetcher(string query, object[] env, IDictionary bindings)
        {
            m_query    = query;
            m_env      = env;
            m_bindings = bindings;
        }

        #endregion

        #region IInvocable implementation

        public override void Run()
        {
            // do nothing, invocable will be executed inside cluster
        }

        #endregion

        #region IPorableObject implementation

        /// <summary>
        /// Restore the contents of a user type instance by reading its state
        /// using the specified <see cref="IPofReader"/> object.
        /// </summary>
        /// <param name="reader">
        /// The <b>IPofReader</b> from which to read the object's state.
        /// </param>
        /// <exception cref="IOException">
        /// If an I/O error occurs.
        /// </exception>
        public override void ReadExternal(IPofReader reader)
        {
            m_fetchExtractor = reader.ReadBoolean(0);
            m_query          = reader.ReadString(1);
            m_env            = (object[]) reader.ReadArray(2, null);
            m_bindings =     (IDictionary) reader.ReadDictionary(3, null);
        }

        /// <summary>
        /// Save the contents of a POF user type instance by writing its
        /// state using the specified <see cref="IPofWriter"/> object.
        /// </summary>
        /// <param name="writer">
        /// The <b>IPofWriter</b> to which to write the object's state.
        /// </param>
        /// <exception cref="IOException">
        /// If an I/O error occurs.
        /// </exception>
        public override void WriteExternal(IPofWriter writer)
        {
            writer.WriteBoolean(0, m_fetchExtractor);
            writer.WriteString(1, m_query);
            writer.WriteArray(2, m_env);
            writer.WriteDictionary(3, m_bindings);
        }

        #endregion

        #region Data members

        /// <summary>
        /// Flag to control whether to get an IValueExtractor vs. an IFilter.
        /// </summary>
        protected bool m_fetchExtractor;

        /// <summary>
        /// The query string to use.
        /// </summary>
        protected string m_query;

        /// <summary>
        /// An array of bindings.
        /// </summary>
        protected object[] m_env;

        /// <summary>
        /// A dictionary of bindings.
        /// </summary>
        protected IDictionary m_bindings;

        #endregion
    }
}
