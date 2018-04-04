package com.tangosol.examples.contacts;

import com.tangosol.examples.pof.Contact;
import com.tangosol.net.cache.CacheStore;
import com.tangosol.util.Base;
import com.tangosol.examples.pof.ContactId;
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


public class DBCacheStore extends Base implements CacheStore
{
    public DBCacheStore()
    {
        m_sTableName = "contact";
        configureConnection();
    }

    protected void configureConnection()
    {
        try
        {
            Class.forName(DB_DRIVER);
            //m_con = DriverManager.getConnection("jdbc:mysql://localhost/euclid?user=root&password=euclid123");
            m_con = DriverManager.getConnection(DB_URL, DB_USERNAME, DB_PASSWORD);
            m_con.setAutoCommit(true);
        }
        catch (Exception e)
        {
            throw ensureRuntimeException(e, "Connection failed");
        }
    }

    public Object load(Object oKey)
    {
        Object     oValue = null;
        Connection con    = getConnection();
        String     sSQL   = "SELECT FirstName, Lastname FROM " + getTableName()
                + " WHERE FirstName = ? AND LastName = ?";

        try
        {
            PreparedStatement stmt = con.prepareStatement(sSQL);
            ContactId id = (ContactId) oKey;
            stmt.setString(1, id.getFirstName());
            stmt.setString(2, id.getLastName());

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

    public void store(Object oKey, Object oValue)
    {
        Connection con     = getConnection();
        String     sTable  = getTableName();
        String     sSQL;

        Contact contact = (Contact) oValue;

        try
        {
            PreparedStatement stmt;

            if (load(oKey) != null)
            {
                /*
                // key exists - update
                sSQL = "UPDATE " + sTable +
                        " SET BirthDate = ?" +
                        " AND Home_StreetlLne1 = ?" +
                        " AND Home_StreetlLne2 = ?" +
                        " AND Home_City = ?" +
                        " AND Home_State = ?" +
                        " AND Home_Zip = ?" +
                        " AND Home_Country = ?" +
                        " AND Home_AccessCode = ?" +
                        " AND Home_CountryCode = ?" +
                        " AND Home_AreaCode = ?" +
                        " AND Home_LocalNumber = ?" +
                        " AND Work_StreetlLne1 = ?" +
                        " AND Work_StreetlLne2 = ?" +
                        " AND Work_City = ?" +
                        " AND Work_State = ?" +
                        " AND Work_Zip = ?" +
                        " AND Work_Country = ?" +
                        " AND Work_AccessCode = ?" +
                        " AND Work_CountryCode = ?" +
                        " AND Work_AreaCode = ?" +
                        " AND Work_LocalNumber = ?" +
                        " WHERE FirstName = ?" +
                        " AND LastName = ?";

                int i = 0;
                stmt = con.prepareStatement(sSQL);
                stmt.setString(++i, contact.getBirthDate().toString());
                stmt.setString(++i, contact.getHomeAddress().getStreet1());
                stmt.setString(++i, contact.getHomeAddress().getStreet2());
                stmt.setString(++i, contact.getHomeAddress().getCity());
                stmt.setString(++i, contact.getHomeAddress().getState());
                stmt.setString(++i, contact.getHomeAddress().getZipCode());
                stmt.setString(++i, contact.getHomeAddress().getCountry());
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getAccessCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getCountryCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getAreaCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getLocalNumber()));
                stmt.setString(++i, contact.getWorkAddress().getStreet1());
                stmt.setString(++i, contact.getWorkAddress().getStreet2());
                stmt.setString(++i, contact.getWorkAddress().getCity());
                stmt.setString(++i, contact.getWorkAddress().getState());
                stmt.setString(++i, contact.getWorkAddress().getZipCode());
                stmt.setString(++i, contact.getWorkAddress().getCountry());
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getAccessCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getCountryCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getAreaCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getLocalNumber()));
                stmt.setString(++i, contact.getFirstName());
                stmt.setString(++i, contact.getLastName());*/
            }
            else
            {
                // new key - insert
                sSQL = "INSERT INTO " + sTable + " VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                int i = 0;
                stmt = con.prepareStatement(sSQL);
                stmt.setString(++i, contact.getFirstName());
                stmt.setString(++i, contact.getLastName());
                stmt.setString(++i, contact.getBirthDate().toString());
                stmt.setString(++i, contact.getHomeAddress().getStreet1());
                stmt.setString(++i, contact.getHomeAddress().getStreet2());
                stmt.setString(++i, contact.getHomeAddress().getCity());
                stmt.setString(++i, contact.getHomeAddress().getState());
                stmt.setString(++i, contact.getHomeAddress().getZipCode());
                stmt.setString(++i, contact.getHomeAddress().getCountry());
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getAccessCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getCountryCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getAreaCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("home").getLocalNumber()));
                stmt.setString(++i, contact.getWorkAddress().getStreet1());
                stmt.setString(++i, contact.getWorkAddress().getStreet2());
                stmt.setString(++i, contact.getWorkAddress().getCity());
                stmt.setString(++i, contact.getWorkAddress().getState());
                stmt.setString(++i, contact.getWorkAddress().getZipCode());
                stmt.setString(++i, contact.getWorkAddress().getCountry());
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getAccessCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getCountryCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getAreaCode()));
                stmt.setString(++i, String.valueOf(contact.getPhoneNumbers().get("work").getLocalNumber()));

                stmt.executeUpdate();
                stmt.close();
            }

        }
        catch (SQLException e)
        {
            System.out.print(e.getMessage());
        }
    }

    public void erase(Object oKey)
    {
        System.out.println("load");
        return;
    }

    public void eraseAll(Collection colKeys)
    {
        throw new UnsupportedOperationException();
    }

    public Map loadAll(Collection colKeys)
    {
        throw new UnsupportedOperationException();
    }

    public void storeAll(Map mapEntries)
    {
        throw new UnsupportedOperationException();
    }


    public Iterator keys()
    {

        throw new UnsupportedOperationException("Sandeep Jaiswal2");
    }

    public String getTableName()
    {
        return m_sTableName;
    }

    public Connection getConnection()
    {
        return m_con;
    }


    protected Connection m_con;

    protected String m_sTableName;

    private static final String DB_DRIVER = "org.gjt.mm.mysql.Driver";

    private static final String DB_URL = "jdbc:mysql://localhost:3306/euclid";

    private static final String DB_USERNAME = "root";

    private static final String DB_PASSWORD = "euclid123";
}
