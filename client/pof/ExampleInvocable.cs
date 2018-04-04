/*
 * #%L
 * ExampleInvocable.cs
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
﻿using System;
using Tangosol.IO.Pof;
using Tangosol.Net;

namespace Tangosol.Examples.Pof
{
    public class ExampleInvocable : IInvocable
    {
        public void Run()
        {
        }

        public void ReadExternal(IPofReader reader)
        {
        }

        public void WriteExternal(IPofWriter writer)
        {
        }

        public void Init(IInvocationService service)
        {
        }

        public object Result
        {
            get { return 42; }
        }
    }
}
