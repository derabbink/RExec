# RExec
Demo app displaying different ways of Remote Execution.<br/>
Communication happens over WCF.

This is a work in progress.


## How To Run

 * Open `RExec.sln` in Visual Studio (>= 2010).
 * run `RExec.Host` first.
 * run `RExec.Client`
 * Check the client's and server's `App.config` for settings. N.B.: you may need to execute some commands to get permission for opening ports. You can find these in comments in the config files.


## Purpose

This code is for demonstration purposes only. That's also the reason why there are no tests.


## Types Of Remote Execution

Since serialization of delegates is not a good idea, you can't just transport them over the network and invoke them on a remote machine.
The next best option is to use reflection to instruct the remote machine what methods to invoke, based on their name, fully qualified class name and the name of the assembly they are located in.
But for this to work, the assembly containing the method needs to be loaded by the executing machine.

Transporting such an assembly to, and loading it on a remote machine is not yet implemented. **To be continued...**


### Method Invocation Trees

Currently the idea is to test the following kinds of invocation paths (nested bullet lists are supposed to be a tree structure):

 * host assembly `RExec.Host.InternalInstructions`
  * (same) host assembly `RExec.Host.InternalInstructions.InternalDependency`
  * referenced assembly `Instructions.Reference.Host.ExternalDependency`
 * referenced assembly `Instructions.Reference.Host`
  * (same) referenced assembly `Instructions.Reference.Host.InternalDependency`
  * referenced assembly to the referenced assembly `Instructions.Reference.Host.Dependency`

**not implemented yet:** (but some parts are in the code already)

 * client assembly `RExec.Client.InternalInstructions`
  * (same) client assembly `RExec.Client.InternalInstructions.InternalDependency`
  * referenced assembly `Instructions.Reference.Client.ExternalDependency`
 * referenced assembly `Instructions.Reference.Client`
  * (same) referenced assembly `Instructions.Reference.Client.InternalDependency`
  * referenced assembly to the referenced assembly `Instructions.Reference.Client.Dependency`
 * independent assembly
  * (same) independent assembly
  * referenced assembly to the independent assembly

This type of approach causes a lot of tiny code clones, since reusing classes will introduce assembly-dependencies which we *don't* want.
