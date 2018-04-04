/*
 * #%L
 * FilterFactory.cs
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
using System;
using System.Collections;
using Tangosol.Examples.Pof;
using Tangosol.Net;
using Tangosol.Util;

namespace Tangosol.Examples.Contacts
{
    /// <summary>
    /// <b>FilterFactory</b> is a utility class that provides a set of
    /// factory methods that are used for building instances of 
    /// <see cref="IFilter"/> or <see cref="IValueExtractor"/>.
    /// </summary>
    /// <remarks>
    /// <p>
    /// We use an <b>IInvocationService</b> to build the Filters and
    /// ValueExtractors on a java proxy server. This class provides an
    /// example of using the IInvocationService to call <b>QueryHelper</b>.</p>
    /// <p>
    /// The FilterFactory API accepts a String that specifies the creation of
    /// rich Filters in a format that should be familiar to anyone that
    /// understands SQL WHERE clauses. For example the String "street = 'Main'
    /// and state = 'TX'" would create a tree of Filters that is the same as
    /// the following code:
    /// 
    /// <code>
    /// new AndFilter(
    ///     new EqualsFilter("getStreet","Main"),
    ///     new EqualsFilter("getState","TX"));
    /// </code>
    /// 
    /// <see cref="QueryHelper"/>
    /// <p>
    /// The factory methods catch a number of Exceptions from the
    /// implementation stages and subsequently may throw an unchecked 
    /// FilterBuildingException.</p>
    /// </remarks>
    /// <author>Ivan Cikic  2010.03.09</author>
    public class FilterFactory
    {
        #region Constructors

        /// <summary>
        /// Construct a FilterFactory instance.
        /// </summary>
        public FilterFactory()
        {
        }

        /// <summary>
        /// Construct a FilterFactory instance and set the service name of
        /// the IInvocationService.
        /// </summary>
        /// <param name="serviceName">
        /// The name of IInvocationService to use.
        /// </param>
        public FilterFactory(string serviceName)
        {
            m_serviceName = serviceName;
            try
            {
                m_service = (IInvocationService) CacheFactory.GetService(serviceName);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Exception hooking up to service: " + serviceName);
                Console.WriteLine(e);
                m_service = null;
            }
        }

        #endregion

        #region FilterBuilder API

        /// <summary>
        /// Make a new IFilter from the given string.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing an IFilter.
        /// </param>
        /// <returns>The constructed IFilter.</returns>
        public IFilter CreateFilter(string s)
        {
            return FetchFilter(s, null, null);
        }

        /// <summary>
        /// Make a new IFilter from the given String.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing an IFilter.
        /// </param>
        /// <param name="env">The array of objects to use for bind variables.</param>
        /// <returns>The constructed IFilter</returns>
        public IFilter CreateFilter(string s, object[] env)
        {
            return FetchFilter(s, env, null);
        }

        /// <summary>
        /// Make a new IFilter from the given String.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing an IFilter.
        /// </param>
        /// <param name="bindings">
        /// The dictionary of objects to use for bind variables.
        /// </param>
        /// <returns>The constructed IFilter</returns>
        public IFilter CreateFilter(string s, IDictionary bindings)
        {
            return FetchFilter(s, null, bindings);
        }

        /// <summary>
        /// Make a new IFilter from the given String.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing an IFilter.
        /// </param>
        /// <param name="env">
        /// The array of object to use for bind variables.
        /// </param>
        /// <param name="bindings">
        /// The dictionary of objects to use for bind variables.
        /// </param>
        /// <returns>The constructed IFilter</returns>
        public IFilter CreateFilter(String s, object[] env, IDictionary bindings)
        {
            return FetchFilter(s, env, bindings);
        }

        /// <summary>
        /// Make a new IValueExtracter from the given String.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing a
        /// IValueExtractor.
        /// </param>
        /// <returns>The constructed IValueExtractor.</returns>
        public IValueExtractor CreateExtractor(string s)
        {
            return FetchExtractor(s);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Make a new IFilter from the given string using IInvocationService.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing an IFilter.
        /// </param>
        /// <param name="env">
        /// The array of Objects to use for Bind variables.
        /// </param>
        /// <param name="bindings">
        /// The dictionary of objects to use for Bind variables.
        /// </param>
        /// <returns>The constructed IFilter.</returns>
        public IFilter FetchFilter(string s, object[] env, IDictionary bindings)
        {
            if (m_service == null)
            {
                return null;
            }
            IDictionary result = m_service.Query(new FilterFetcher(s, env, bindings), null);
            if (result.Values == null)
                return null;

            IEnumerator enumerator = result.Values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                return (IFilter) enumerator.Current;
            }
            return null;
        }

        /// <summary>
        /// Make a new IValueExtracter from the given String.
        /// </summary>
        /// <param name="s">
        /// String in the Coherence Query Language representing a 
        /// IValueExtractor
        /// </param>
        /// <returns>The constructed IValueExtractor.</returns>
        public IValueExtractor FetchExtractor(string s)
        {
            if (m_service == null)
            {
                return null;
            }
            IDictionary result = m_service.Query(new FilterFetcher(s, true), null);
            if (result.Values == null)
                return null;

            IEnumerator enumerator = result.Values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                return (IValueExtractor) enumerator.Current;
            }
            return null;
        }

        #endregion

        #region Data members

        /// <summary>
        /// The service name to use.
        /// </summary>
        private string m_serviceName;

        /// <summary>
        /// The invocation service.
        /// </summary>
        protected IInvocationService m_service;

        #endregion
    }
}
