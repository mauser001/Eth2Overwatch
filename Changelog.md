# Eth2Overwatch - Changelog

#### Version 1.0.7-RC.2
+ Bugfix: Small Bugfix for downloading the latest version

#### Version 1.0.7-RC.1
+ Feature: Added support for custom version

#### Version 1.0.6-RC.2
+ Bugfix: try to fix downloading of the new Version

#### Version 1.0.6-RC.1
+ Display used Version of Validator and Beacon-Chain
+ Report Version of Validator

#### Version 1.0.5-RC.4
+ Bugfix: Send more reporing Data

#### Version 1.0.5-RC.3
+ Bugfix: Reload config after download of now prysm software

#### Version 1.0.5-RC.2
+ Feature: (Re)added the checkbox to disable the local eth1 connection

#### Version 1.0.5-RC.1
+ Feature: Added the possibility to send report data to external url

#### Version 1.0.4-RC.2
+ Fix: added initial delay for validator, that the beacon chain has time to start
+ Change: Modified retry-delays

#### Version 1.0.4-RC.1
+ Fix: Textnet is now cofigurable and no longer fixed with medalla
+ Removed optional connection to prysm görli eth1 node. Local eth1 client is now mandatory.

#### Version 1.0.3-Beta.2
+ Fix: Adapted key import according to last prysm changes 
  + renamed account-v2 to account
  + added medalla flag to import

#### Version 1.0.3-Beta.1
+ Change: added --medalla flag, when using the testnet
+ Change: added --accept-terms-of-use flag

#### Version 1.0.2-Beta.5
+ Features
  + Added Validator details

#### Version 1.0.2-Beta.4
+ Features
  + Added total balance (Summ of all local Validators Balances)
  + Added Validator state summary

#### Version 1.0.2-Beta.3
+ Features  
  + Added Log-Message Filter (Error, Warning, Info)
  + Hide Log-Messages if console window is not hidden
+ Bugfixes
  + Dont't restart processes if healthz state is not awailable, but the process is still running.
+ Update doku

#### Version 1.0.2-Beta.1
+ Refactoring 
  + Loading and executing the validator/beacon chain directly and not via the prysm.bat
+ Feature
  + Checking now if latest version is downloaded and if not download them

#### Version 1.0.1-Beta.2
+ Features
  + Folder Selection Buttons added
  + Initial Setup for Eth2 beacon chain and validator added
#### Version 1.0.1-Beta.2
+ Features
    + Auto start after windows login added
    + Added Icon
+ Fixes
    + Check if executables and folder exists
    + minor code improvements
#### Version 1.0.1-Beta.1
+ Changes
  + Added medalla testnet support
    + Including the import of medalla validator account
#### Version 1.0.1-Beta.2
+ Bugfix
  + Addapted changes from prysm v18