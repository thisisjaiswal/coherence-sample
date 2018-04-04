@echo off
setlocal enabledelayedexpansion

set COHERENCE=C:\Oracle\Middleware\Oracle_Home\coherence\lib\coherence.jar
set JAVA="C:\Program Files\Java\jdk1.8.0_144\bin\java"

set CLASSES_DIR=classes 
  

set CP=-cp %COHERENCE%;%CLASSES_DIR%
set COH_OPTS=-Dcoherence.cacheconfig=examples-cache-config.xml -Dcoherence.pofconfig=_pof-config.xml -Dcoherence.wka=127.0.0.1 -Dcoherence.clusterport=5555 

:run
%JAVA% %CP% %COH_OPTS% -Xms512m -Xmx512m com.tangosol.net.DefaultCacheServer

:exit
