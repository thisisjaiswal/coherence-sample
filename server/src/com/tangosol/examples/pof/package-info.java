/*
 * #%L
 * package-info.java
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
/**
 * Required because of lack of support for java.time.* in JAXB
 * in Java 8.
 *
 * @author tam  2015.07.10
 * @since 12.2.1
 */

@XmlJavaTypeAdapters({@XmlJavaTypeAdapter(type = LocalDate.class, value = LocalDateXmlAdapter.class)})

package com.tangosol.examples.pof;

import java.time.LocalDate;
import javax.xml.bind.annotation.adapters.XmlJavaTypeAdapter;
import javax.xml.bind.annotation.adapters.XmlJavaTypeAdapters;
