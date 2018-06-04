package com.nomura.euclid;

import com.tangosol.net.DefaultCacheServer;

public class Main {

    public static void main(String[] args) {
        DefaultCacheServer.main(new String[0]);
    }

}
// -Dcoherence.distributed.persistence.mode=active
// -Dcoherence.cacheconfig=cache-config.xml