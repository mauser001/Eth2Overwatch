# Eth2Overwatch - Testnet
Windows tool to manage processes for the prysm eth2 beacon chain, pryms eth2 validator and eth1 geth client.
The Prysm clients will be downlaoded and used. 
The Geth client must be updated manually. 

![alt text](http://maushammer.at/eth2/Eth2Overwatch.png "Screenshot")

### License:
MIT - The Program and Code is free to use/modify/copy

### Disclaimer: 
I do not take any responsability for any damage/problems caused by the Eth2Overwatch tool. But I do my best to awoid them!

It is not possible to run more then one Eth1 (geth), Eth2 Beacon Chains or Eth2 Validators on the same machine as this programm kills all geth/beacon-chain/validator processes before starting new ones.

If find any spelling errors in the readme -> I write code - not books ;-)

When using this software you agree to the prysm terms of service: https://github.com/prysmaticlabs/prysm/blob/master/TERMS_OF_SERVICE.md

### Framework and tools used
+ Visiual Studio 2019 Prview.
  + .net Core 3.1
+ NuGet Packages:
  + Nethereum.Geth

### Requirements/Prerequisites:
+ Lauchpad account creation completed: https://launchpad.ethereum.org/
+ If you want to run a local Eth1 node (not required for eth2)
  + Installed Geth client (I used https://github.com/ethereum/go-ethereum )


Latest compiled win10 version can be found at:
https://github.com/mauser001/Eth2Overwatch/tree/master/LatestRelease
(.net Core 3.1 Framework has to be installed)

## Setup
Download the Eth2Overwatch.exe in a folder and start ist.

### Setting
+ ##### Initial Eth2 Setup (can also be downloaded to accquire updates)
  + Press the "Eth2 setup ..." btton
  + Select a folder wehre the the prysm executable files (beacon chain and validator) will be stored
  + Press "2. Download beacon chain executable" to download the file
  + Press "3. Download validator executable" to download the file
  + Import account
    + 4. Complete the lauchpad account creation: https://launchpad.ethereum.org/
    + 5. Pick the folder where you store your keys
    + 6. Pick a folder where to store your wallet
    + 7. Click on 'Import account' to import the account
      + Enter your key password.
      + If you get the message, that at least 1 account was imported then your are good to go.
+ ##### Global: 
  + Eth2 Testnet: If empty the clients connect to the main net. If not empty they connect to the specified test net. For eth1 g�rli is always used.
  + Start on Windows start: If checked the Eth2Overseer starts after win. login.
  + Use local eth1 connection:
    + If enabled: Make sure you also use the eth1 node with the overseer
    + If disabled: You will need to define a --execution-endpoin in the additional commands of the beacon chain window
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
  + Version:
    + Latest
      + Checked: The latest Prysm version is used
      + Not checked: Enter the version you want to use. The tool will try to download it if not available in the folder.
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
  + Version:
    + Latest
      + Checked: The latest Prysm version is used
      + Not checked: Enter the version you want to use. The tool will try to download it if not available in the folder.
  + Password path: Path to a textfile holding your wallet password (plaintext).
  + Hide cmd:
    + Checked: Process runs in the background
    + Not checked: cmd window running the Validator process will be visible
  + Executlabe folder path: Path where the prysm.bat is located
  + Wallet path: Path where validator wallet data should be stored
  + Additional commands: Additional command line parameters for calling the prysm.bat
  + Result window: Shows the state of the Validator.
    + Green: Connection to the Validator could be established and the health state is shown from: http://localhost:8081/healthz
    + Red: There was an error starting the eth2 Validator
  + Details Button: Open a Window with Details of local validators.
    + You can specify an external url to send the report data to a remote adress.
      + The key must match a key defined on the server
      + PHP Script and example website to display the report data can be found here:
        https://github.com/mauser001/validator-report

### Roadmap
+ Design
+ Creating a new Validator account
+ More complex status checking
