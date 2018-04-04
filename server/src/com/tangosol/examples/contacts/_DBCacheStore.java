package com.tangosol.examples.contacts;


import com.tangosol.net.cache.CacheStore;
import com.tangosol.util.Base;

import java.sql.DriverManager;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;


/**
 * An example implementation of CacheStore
 * interface.
 *
 * @author erm 2003.05.01
 */
public class _DBCacheStore
        extends Base
        implements CacheStore
{
    // ----- constructors ---------------------------------------------------
    /**
     * Constructs _DBCacheStore for a given database table.
     *
     * @param sTableName the db table name
     */
    public _DBCacheStore(String sTableName)
    {
        m_sTableName = sTableName;
        configureConnection();
    }

    /**
     * Set up the DB connection.
     */
    protected void configureConnection()
    {
        try
        {
            Class.forName("org.gjt.mm.mysql.Driver");
            m_con = DriverManager.getConnection(DB_URL, DB_USERNAME, DB_PASSWORD);
            m_con.setAutoCommit(true);
        }
        catch (Exception e)
        {
            throw ensureRuntimeException(e, "Connection failed");
        }
    }


    // ---- accessors -------------------------------------------------------

    /**
     * Obtain the name of the table this CacheStore is persisting to.
     *
     * @return the name of the table this CacheStore is persisting to
     */
    public String getTableName()
    {
        return m_sTableName;
    }

    /**
     * Obtain the connection being used to connect to the database.
     *
     * @return the connection used to connect to the database
     */
    public Connection getConnection()
    {
        return m_con;
    }


    // ----- CacheStore Interface --------------------------------------------

    /**
     * Return the value associated with the specified key, or null if the
     * key does not have an associated value in the underlying store.
     *
     * @param oKey  key whose associated value is to be returned
     *
     * @return the value associated with the specified key, or
     *         <tt>null</tt> if no value is available for that key
     */
    public Object load(Object oKey)
    {
        Object     oValue = null;
        Connection con    = getConnection();
        String     sSQL   = "SELECT id, value FROM " + getTableName()
                + " WHERE id = ?";
        try
        {
            PreparedStatement stmt = con.prepareStatement(sSQL);

            stmt.setString(1, String.valueOf(oKey));

            ResultSet rslt = stmt.executeQuery();
            if (rslt.next())
            {
                oValue = rslt.getString(2);
                if (rslt.next())
                {
                    throw new SQLException("Not a unique key: " + oKey);
                }
            }
            stmt.close();
        }
        catch (SQLException e)
        {
            throw ensureRuntimeException(e, "Load failed: key=" + oKey);
        }
        return oValue;
    }

    /**
     * Store the specified value under the specific key in the underlying
     * store. This method is intended to support both key/value creation
     * and value update for a specific key.
     *
     * @param oKey    key to store the value under
     * @param oValue  value to be stored
     *
     * @throws UnsupportedOperationException  if this implementation or the
     *         underlying store is read-only
     */
    public void store(Object oKey, Object oValue)
    {
        Connection con     = getConnection();
        String     sTable  = getTableName();
        String     sSQL;

        // the following is very inefficient; it is recommended to use DB
        // specific functionality that is, REPLACE for MySQL or MERGE for Oracle
        if (load(oKey) != null)
        {
            // key exists - update
            sSQL = "UPDATE " + sTable + " SET value = ? where id = ?";
        }
        else
        {
            // new key - insert
            sSQL = "INSERT INTO " + sTable + " (value, id) VALUES (?,?)";
        }
        try
        {
            PreparedStatement stmt = con.prepareStatement(sSQL);
            int i = 0;
            stmt.setString(++i, String.valueOf(oValue));
            stmt.setString(++i, String.valueOf(oKey));
            stmt.executeUpdate();
            stmt.close();
        }
        catch (SQLException e)
        {
            throw ensureRuntimeException(e, "Store failed: key=" + oKey);
        }
    }

    /**
     * Remove the specified key from the underlying store if present.
     *
     * @param oKey key whose mapping is to be removed from the map
     *
     * @throws UnsupportedOperationException  if this implementation or the
     *         underlying store is read-only
     */
    public void erase(Object oKey)
    {
        Connection con  = getConnection();
        String     sSQL = "DELETE FROM " + getTableName() + " WHERE id=?";
        try
        {
            PreparedStatement stmt = con.prepareStatement(sSQL);

            stmt.setString(1, String.valueOf(oKey));
            stmt.executeUpdate();
            stmt.close();
        }
        catch (SQLException e)
        {
            throw ensureRuntimeException(e, "Erase failed: key=" + oKey);
        }
    }

    /**
     * Remove the specified keys from the underlying store if present.
     *
     * @param colKeys  keys whose mappings are being removed from the cache
     *
     * @throws UnsupportedOperationException  if this implementation or the
     *         underlying store is read-only
     */
    public void eraseAll(Collection colKeys)
    {
        throw new UnsupportedOperationException();
    }

    /**
     * Return the values associated with each the specified keys in the
     * passed collection. If a key does not have an associated value in
     * the underlying store, then the return map does not have an entry
     * for that key.
     *
     * @param colKeys  a collection of keys to load
     *
     * @return a Map of keys to associated values for the specified keys
     */
    public Map loadAll(Collection colKeys)
    {
        throw new UnsupportedOperationException();
    }

    /**
     * Store the specified values under the specified keys in the underlying
     * store. This method is intended to support both key/value creation
     * and value update for the specified keys.
     *
     * @param mapEntries   a Map of any number of keys and values to store
     *
     * @throws UnsupportedOperationException  if this implementation or the
     *         underlying store is read-only
     */
    public void storeAll(Map mapEntries)
    {
        throw new UnsupportedOperationException();
    }

    /**
     * Iterate all keys in the underlying store.
     *
     * @return a read-only iterator of the keys in the underlying store
     */
    public Iterator keys()
    {
        Connection con  = getConnection();
        String     sSQL = "SELECT id FROM " + getTableName();
        List       list = new LinkedList();

        try
        {
            PreparedStatement stmt = con.prepareStatement(sSQL);
            ResultSet         rslt = stmt.executeQuery();
            while (rslt.next())
            {
                Object oKey = rslt.getString(1);
                list.add(oKey);
            }
            stmt.close();
        }
        catch (SQLException e)
        {
            throw ensureRuntimeException(e, "Iterator failed");
        }

        return list.iterator();
    }


    // ----- data members ---------------------------------------------------

    /**
     * The connection.
     */
    protected Connection m_con;

    /**
     * The db table name.
     */
    protected String m_sTableName;

    /**
     * Driver class name.
     */
    private static final String DB_DRIVER = "org.gjt.mm.mysql.Driver";

    /**
     * Connection URL.
     */
    private static final String DB_URL = "jdbc:mysql://localhost:3306/CacheStore";

    /**
     * User name.
     */
    private static final String DB_USERNAME = "root";

    /**
     * Password.
     */
    private static final String DB_PASSWORD = null;
}
