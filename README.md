# Eth2Overwatch - Testnet
Windows tool to manage processes for eth1 (goerli test) node, eth2 beacon chain (Testnet) and eth2 validator (Testnet)

At the moment it is not possible to link the lokal eth1 chain with the beacon chain. The beacon chain conntects to https://goerli.prylabs.net




![alt text](http://maushammer.at/eth2/Eth2Overwatch.png "Screenshot")

### License:
MIT - The Program and Code is free to use/modify/copy

### Disclaimer: 
I do not take any responsability for any damage/problems caused by the Eth2Overwatch tool. But I do my best to awoid them!

It is not possible to run more then one Eth1 (geth), Eth2 Beacon Chains or Eth2 Validators on the same machine as this programm kills all geth/beacon-chain/validator processes before starting new ones.

If find any spelling errors in the readme -> I write code - not books ;-)

### Framework and tools used
+ Visiual Studio 2019 Prview.
  + .net Core 3.1
+ NuGet Packages:
  + Nethereum.Geth

### Requirements/Prerequisites:
+ Medalla lauchpad account creation completed: https://medalla.launchpad.ethereum.org/overview
+ If you want to run a local Eth1 node (not required for eth2)
  + Installed Geth client (I used https://github.com/ethereum/go-ethereum )


Latest compiled win10 version can be found at:
https://github.com/mauser001/Eth2Overwatch/tree/master/LatestRelease
(.net Core 3.1 Framework has to be installed)

## Setup
Download the Eth2Overwatch.exe in a folder and start ist.

### Setting
+ ##### Initial Eth2 Setup (can also be downloaded to accquire updates)
  + Press the "Initial Eth2 setup" btton
  + Select a folder wehre the prysm.bat file should be stored
  + Press "2. Download prysm.bat" to download the file
    + If you check 'Delete existing files' all content in the prysm folder will be deletet. This is recomended for updates!
  + Import Medalla account
    + Complete the Medalla lauchpad account creation: https://medalla.launchpad.ethereum.org/overview
    + Pick the folder where you store your medalla keys
    + Pick a folder where to store your wallet
    + Pick a folder where to store your password (clear text)
    + Click on 'Import Medalla account' to import the account
    + Enter your medalla key password.
    + If you get the message, that at least 1 account was imported then your are good to go.
+ ##### Global: 
  + Use Görli test net: (atm. only for Eth1, as Eth2 is only on test net). If you deactive this checkbox you could run the eth1 main net node.
  + [not working atm] Connect with Eth1 node. 
    + Checked: Local Eth1 client and Eth2 Beacon Chain are connected
    + Not checked: Eth2 Beacon Chain Connects to default Eth Chain (https://goerli.prylabs.net)
  + Start on Windows start: If checked the Eth2Overseer starts after win. login.
+ ##### Eth1
  + Start Eth1: Stops all existing Eth1 (geth) processes and starts a new one
  + Stop Eth1: Stops all existing Eth1 (geth) processes 
  + Autostart: Starts the Eth1 Process (if not started) on Start of this Programm
  + Hide cmd:
    + Checked: Process runs in the background
    + Not checked: cmd window running the Eth1 process will be visible
  + Executlabe folder path: Path where the geth.exe is located
  + Data path: Path where the eth1 chain data should be stored
  + Additional commands: Additional command line parameters for calling the geth.exe
  + Result window: Shows the state of the eth1 chain.
    + Green: Connection to the chain could be established and the latest syncted block is shown
    + Red: There was an error starting the eth1 chain
+ ##### Beacon Chain - Eth2
  + Start Beacon: Stops all existing Beacon chain (beacon) processes and starts a new one
  + Stop Beacon: Stops all existing Beacon chain (beacon) processes 
  + Autostart: Starts the Beacon chain Process (if not started) on Start of this Programm
  + Hide cmd:
    + Checked: Process runs in the background
    + Not checked: cmd window running the Beacon chain process will be visible
  + Executlabe folder path: Path where the prysm.bat is located
  + Data path: Path where the eth2 chain data should be stored
  + Additional commands: Additional command line parameters for calling the prysm.bat
  + Result window: Shows the state of the eth2 chain.
    + Green: Connection to the chain could be established and the health state is shown from: http://localhost:8080/healthz
    + Red: There was an error starting the eth2 beacon chain
+ ##### Validator - Eth2
  + Start Validator: Stops all existing Validator (validator) processes and starts a new one
  + Stop Validator: Stops all existing Validator chain (validator) processes 
  + Autostart: Starts the Validator Process (if not started) on Start of this Programm
  + Password file path: Path where the validator password files are stored.
  + Hide cmd:
    + Checked: Process runs in the background
    + Not checked: cmd window running the Validator process will be visible
  + Executlabe folder path: Path where the prysm.bat is located
  + Wallet path: Path where validator wallet data should be stored
  + Additional commands: Additional command line parameters for calling the prysm.bat
  + Result window: Shows the state of the Validator.
    + Green: Connection to the Validator could be established and the health state is shown from: http://localhost:8081/healthz
    + Red: There was an error starting the eth2 Validator


### Roadmap
+ Design
+ Creating a new Validator account
+ More complex status checking
+ Displaying of stats

### Known issues/bugs:
+ Start on Windows Startup does not work
+ Local connection between eth1 node and beacon chain not working