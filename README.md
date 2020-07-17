# Eth2Overwatch - Testnet
Windows tool to manage processes for eth1 (goerli test) node, eth2 beacon chain (Testnet) and eth2 validator (Testnet)

![alt text](http://maushammer.at/eth2/Eth2Overwatch.png "Screenshot")

### License:
MIT - The Program and Code is free to use/modify/copy

### Disclaimer: 
This is my first C# winforms project.

I do not take any responsability for any damage/problems caused by the Eth2Overwatch tool. But I do my best to awoid them!

It is not possible to run additional Eth1 (geth), Eth2 Beacon Chains or Eth2 Validators on the same machine as this programm kills all geth/beacon-chain/validator processes before starting new ones.

### Framework and tools used
+ Visiual Studio 2019 Prview.
  + .net Core 3.1
+ NuGet Packages:
  + Nethereum.Geth

### Requirements/Prerequisites:
+ Eth1 node
  + Installed Geth client (I used https://github.com/ethereum/go-ethereum )
+ Eth2 beacon chain and validator
  + Setup according to https://prylabs.net/participate (including Validator activation)
+ .net Core installed on the machine: https://dotnet.microsoft.com/download/dotnet-core/3.1

Latest compiled win10 version can be found at:
https://github.com/mauser001/Eth2Overwatch/tree/master/LatestRelease
(it is possible that .net Core 3.1 Framework has to be installed)

## Setup
Copy the Eth2Overwatch.exe in a folder and start ist.

### Setting
+ ##### Global: 
  + Use Görli test net: (atm. only for Eth1, as Eth2 is only on test net). If you deactive this checkbox you could run the eth1 main net node.
  + Connect with Eth1 node. 
    + Checked: Local Eth1 client and Eth2 Beacon Chain are connected
    + Not checked: Eth2 Beacon Chain Connects to default Eth Chain (https://goerli.prylabs.net)
  + Start on windows start (Program sets win registry to be started on startup) ... not working atm.
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
  + Password: Validator password. The password is stored encrypted on the machine. It is only decrypted when the Validator process ist started. When the program is restarted and the password was already set, there will be three *** shown in the password field as placeholders.
  + Key file path: Path where the validator key files are stored, which where created when the validator account was created.
  + Hide cmd:
    + Checked: Process runs in the background
    + Not checked: cmd window running the Validator process will be visible
  + Executlabe folder path: Path where the prysm.bat is located
  + Data path: Path where the eth2 chain data should be stored
  + Additional commands: Additional command line parameters for calling the prysm.bat
  + Result window: Shows the state of the Validator.
    + Green: Connection to the Validator could be established and the health state is shown from: http://localhost:8081/healthz
    + Red: There was an error starting the eth2 Validator


### Roadmap
+ Design
+ Finding a more secure way to pass the password to the validator (maybe with e future version of the prysm software)
+ Installing and Updating geth and pryms executables
+ Creating a new Validator account
+ More complex status checking
+ Displaying of stats

### Known issues/bugs:
+ Start on Windows Startup does not work