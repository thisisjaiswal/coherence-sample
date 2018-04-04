/*
 * #%L
 * LambdaExample.java
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
package com.tangosol.examples.java8;

import com.tangosol.examples.contacts.DataGenerator;
import com.tangosol.examples.contacts.ExamplesHelper;
import com.tangosol.examples.pof.Address;
import com.tangosol.examples.pof.Contact;
import com.tangosol.examples.pof.ContactId;
import com.tangosol.net.NamedCache;
import com.tangosol.util.InvocableMap;
import com.tangosol.util.MapListener;
import com.tangosol.util.listener.SimpleMapListener;

import static com.tangosol.examples.contacts.ExamplesHelper.logHeader;
import static com.tangosol.examples.contacts.ExamplesHelper.sleep;

import static com.tangosol.util.Filters.less;
import static com.tangosol.util.Filters.like;

/**
 * Examples of how to use Lambdas in Coherence.
 *
 * @author tam  2015.05.19
 * @since  12.2.1
 */
public class LambdaExample
    {
    // ----- LambdaExample methods ------------------------------------------

    /**
     * Run the Lambda examples.
     *
     * @param cache the cache to use
     */
    public void runExample (NamedCache<ContactId, Contact> cache)
        {
        logHeader("LambdaExamples begin");
        System.out.println("Cache size is " + cache.size());

        logHeader("add SimpleMapListener using lambdas");

        MapListener<ContactId, Contact> listener = new SimpleMapListener<ContactId, Contact>()
                .addInsertHandler((event) -> System.out.println("\ninserted:\n" + event.getNewValue()));

        cache.addMapListener(listener);

        Contact   newContact = DataGenerator.generateContact();
        ContactId contactId  = new ContactId(newContact.getFirstName(),
                newContact.getLastName());

        logHeader("adding new contact");
        cache.put(contactId, newContact);

        // give enough time for messages to be displayed
        sleep(1000L);

        logHeader("entry processors");

        // update the new contacts address
        cache.invoke(contactId, (entry) ->
            {
            Contact contact = entry.getValue();
            Address address = contact.getHomeAddress();
            address.setStreet1("123 James Street");
            address.setState("MA");
            address.setCity("Lexington");
            address.setZipCode("02420");
            entry.setValue(contact);
            return null;
            });

        sleep(1000L);
        logHeader("removing MapListener");
        cache.removeMapListener(listener);

        // change all names to lowercase using static method
        cache.invokeAll(lowerCase());
        ExamplesHelper.displayContactNames(cache, "with all names lowercase");

        // change firstname to uppercase with a EntryProcessor instance
        InvocableMap.EntryProcessor<ContactId, Contact, Void> processor = (entry) ->
                {
                Contact contact = entry.getValue();
                contact.setFirstName(contact.getFirstName().toUpperCase());
                entry.setValue(contact);
                return null;
                };

        cache.invokeAll(processor);
        ExamplesHelper.displayContactNames(cache, "with firstname uppercase");

        sleep(1000L);

        logHeader("removing contact " + contactId);
        cache.remove(contactId);

        logHeader("Filter DSL Examples");
        // display all entries where the Lastname begins with N
        cache.forEach(like(Contact::getLastName, "N%"),
                (key, value) -> System.out.println("\nContact: " + value));

        // display all entries where the Lastname begins with N and age is < 20
        cache.forEach(
                like(Contact::getLastName, "N%").and(less(Contact::getAge, 20)),
                (key, value) -> System.out.println("\nContact: " + value));

        logHeader("LambdaExamples completed");
        }

    // ----- helpers --------------------------------------------------------

    /**
     * Convert the given Contacts names to lower case.
     *
     * @return null
     */
    protected static InvocableMap.EntryProcessor<ContactId, Contact, Void> lowerCase()
        {
        return entry ->
            {
            Contact contact   = entry.getValue();

            contact.setLastName (contact.getLastName().toLowerCase());
            contact.setFirstName(contact.getFirstName().toLowerCase());

            entry.setValue(contact);
            return null;
            };
        }
    }
