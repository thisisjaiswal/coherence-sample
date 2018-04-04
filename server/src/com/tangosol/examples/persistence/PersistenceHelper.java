/*
 * #%L
 * PersistenceHelper.java
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
package com.tangosol.examples.persistence;

import com.oracle.common.base.Blocking;

import com.tangosol.examples.contacts.DataGenerator;
import com.tangosol.examples.pof.Contact;
import com.tangosol.examples.pof.ContactId;

import com.tangosol.net.CacheFactory;
import com.tangosol.net.Cluster;
import com.tangosol.net.NamedCache;
import com.tangosol.net.management.MBeanServerProxy;
import com.tangosol.net.management.Registry;

import com.tangosol.persistence.CachePersistenceHelper;

import com.tangosol.util.Base;

import javax.management.MBeanException;

import java.time.LocalDate;

import java.util.HashMap;
import java.util.Map;

/**
 * Various helper methods for Persistence examples.
 *
 * @author tam  2015.03.20
 * @since  12.2.1
 */
public class PersistenceHelper
    {
    // ----- constructors ---------------------------------------------------

    /**
     * Construct a new PersistenceHelper which can be used to issue
     * persistence related commands for the examples.
     */
    public PersistenceHelper()
        {
        Cluster cluster = CacheFactory.ensureCluster();

        m_registry = cluster.getManagement();

        if (m_registry == null)
            {
            throw new RuntimeException("Unable to retrieve Registry from cluster");
            }

        m_mbsProxy = m_registry.getMBeanServerProxy();

        // wait for registration of Cluster as the registration is done
        // async and may not be complete before our first call after ensureCluster()
        try
            {
            waitForRegistration(m_registry, Registry.CLUSTER_TYPE);
            }
        catch (InterruptedException e)
            {
            throw Base.ensureRuntimeException(e, "Unable to find MBean");
            }
        }

    // ----- helpers --------------------------------------------------------

    /**
     * List the snapshots for the specified service.
     *
     * @param sServiceName   the name of the service to list snapshots for
     *
     * @return the snapshots for the specified service
     */
    public String[] listSnapshots(String sServiceName)
        {
        String[] asSnapshots = (String[]) getAttribute(
                ensureGlobalName(getMBeanName(sServiceName)), "Snapshots");

        return   asSnapshots == null ? NO_SNAPSHOTS : asSnapshots;
        }

    /**
     * Return a list of archived snapshots for a given service.
     *
     * @param sServiceName   the name of the service to query
     *
     * @return a {@link String}[] of archived snapshots for the given service
     */
    public String[] listArchivedSnapshots(String sServiceName)
        {
        return (String[]) m_mbsProxy.invoke(
                ensureGlobalName(getMBeanName(sServiceName)),
                "listArchivedSnapshots",
                new String[0],
                new String[0]);
        }

    /**
     * Resume a given service.
     *
     * @param sServiceName the service to resume
     */
    public void resumeService(String sServiceName)
        {
        m_mbsProxy.invoke(Registry.CLUSTER_TYPE, RESUME_SERVICE,
                          new String[] {sServiceName},
                          new String[] {"java.lang.String"});
        }

    /**
     * Suspend a given service.
     *
     * @param sServiceName the service to suspend
     */
    public void suspendService(String sServiceName)
        {
        m_mbsProxy.invoke(Registry.CLUSTER_TYPE, SUSPEND_SERVICE,
                          new String[] {sServiceName},
                          new String[] {"java.lang.String"});
        }

    /**
     * Issue an operation and wait for the operation to be complete by
     * polling the "Idle" attribute of the PersistenceCoordinator for the service.
     * This method will poll continuously until an "Idle" status has been reached
     * or until timeout set by a calling thread has been raised. e.g.<br>
     * <pre>
     * try (Timeout t = Timeout.after(120, TimeUnit.SECONDS))
     *     {
     *     helper.invokeOperationWithWait("createSnapshot", "snapshot", "Service");
     *     }
     * </pre>
     *
     * @param sOperation    the operation to execute
     * @param sSnapshot     the snapshot name
     * @param sServiceName  the name of the service to execute operation on
     *
     * @throws MBeanException if any MBean related errors
     */
    public void invokeOperationWithWait(String sOperation, String sSnapshot, String sServiceName)
        throws MBeanException
        {
        try
            {
            String sBeanName = ensureGlobalName(getMBeanName(sServiceName));

            m_mbsProxy.invoke(sBeanName, sOperation,
                new String[] {sSnapshot}, new String[] {"java.lang.String"});

            while (true)
                {
                Blocking.sleep(SLEEP_TIME);

                if ((boolean) getAttribute(sBeanName, "Idle"))
                    {
                    // idle means the operation has completed as we are guaranteed an up-to-date
                    // attribute value just after an operation was called
                    return;
                    }
                }
            }
        catch (Exception e)
            {
            throw Base.ensureRuntimeException(e,
                "Unable to complete operation " + sOperation + " for service " + sServiceName);
            }
        }

    /**
     * Wait for the given MBean to be registered.
     *
     * @param registry    registry to use
     * @param sMBeanName  the MBean to wait for
     *
     * @throws InterruptedException if the Mbean is not registered
     */
    public static void waitForRegistration(Registry registry, String sMBeanName)
            throws InterruptedException
        {
        int nMaxRetries = 100;

        while (!registry.getMBeanServerProxy().isMBeanRegistered(sMBeanName))
            {
            Blocking.sleep(100L);

            if (--nMaxRetries == 0)
                {
                throw new RuntimeException("Unable to find registered MBean " + sMBeanName);
                }
            }
        }

    /**
     * Populate contacts cache with data.
     *
     * @param nc     the cache to populate
     * @param nCount the number of entries to populate
     */
    public static void populateData(NamedCache<ContactId, Contact> nc, int nCount)
        {
        Map<ContactId, Contact> mapContacts = new HashMap<>();

        for (int i = 0; i < nCount; i++)
            {
            Contact contact = new Contact("John", "Smith-" + i, DataGenerator.generateHomeAddress(),
                                          DataGenerator.generateWorkAddress(), null,
                                          LocalDate.now());

            mapContacts.put(new ContactId(contact.getFirstName(), contact.getLastName()), contact);
            }

        nc.putAll(mapContacts);
        }

    /**
     * Return the PersistenceManager MBean name.
     *
     * @param sServiceName  the name of the service to return the name for
     *
     * @return the MBean name
     */
    public static String getMBeanName(String sServiceName)
        {
        return CachePersistenceHelper.getMBeanName(sServiceName);
        }

    /**
     * Return a global name for the given MBean Name.
     *
     * @param sName  the MBean to get global name for.
     *
     * @return the global name.
     */
    private String ensureGlobalName(String sName)
        {
        return m_registry.ensureGlobalName(sName);
        }

    /**
     * Return an attribute name from an MBean.
     *
     * @param sObjectName  object name to query
     * @param sAttribute   attribute to retrieve from object name
     *
     * @return the value of the attribute
     */
    private Object getAttribute(String sObjectName, String sAttribute)
        {
        return m_mbsProxy.getAttribute(sObjectName, sAttribute);
        }

    // ----- constants ------------------------------------------------------

    /**
     * JMX operation to create a snapshot.
     */
    public static final String CREATE_SNAPSHOT = "createSnapshot";

    /**
     * JMX operation to recover a snapshot.
     */
    public static final String RECOVER_SNAPSHOT = "recoverSnapshot";

    /**
     * JMX operation to remove a snapshot.
     */
    public static final String REMOVE_SNAPSHOT = "removeSnapshot";

    /**
     * JMX operation to archive a snapshot
     */
    public static final String ARCHIVE_SNAPSHOT = "archiveSnapshot";

    /**
     * JMX operation to retrieve an archived snapshot
     */
    public static final String RETRIEVE_ARCHIVED_SNAPSHOT = "retrieveArchivedSnapshot";

    /**
     * JMX operation to remove an archived snapshot
     */
    public static final String REMOVE_ARCHIVED_SNAPSHOT = "removeArchivedSnapshot";

    /**
     * JMX operation to suspend a service.
     */
    public static final String SUSPEND_SERVICE = "suspendService";

    /**
     * JMX operation to resume a service.
     */
    public static final String RESUME_SERVICE = "resumeService";

    /**
     * Sleep time between checking operation completion.
     */
    private static final long SLEEP_TIME = 500L;

    /**
     * Signifies no snapshots were found.
     */
    private static final String[] NO_SNAPSHOTS = new String[0];

    // ----- data members ---------------------------------------------------

    /**
     * MBean server proxy for JMX operations and attribute retrieval for online mode.
     */
    private MBeanServerProxy m_mbsProxy;

    /**
     * Management Registry if we are connected to a cluster.
     */
    private Registry m_registry;
    }
