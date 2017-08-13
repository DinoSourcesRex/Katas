# Katas
Consolidating all of the code katas / tests I do so that they're easier to find.

#### Restoring
> `Update-Package -reinstall`

If you have any issues with the NuGet restore run `Update-Package -reinstall` from the Package Manager Console to re-path the hintpaths in the .csproj files.

Doing this will mean that Ncrunch / the projects themselves might break due to an old reference to the Microsoft.Net.Compilers package. I'll fix this one day, I swear.

# List

* RoverExercise - An agent that moves over a grid, following commands. `NUnit, Specflow and Rhinomocks`
* VendingMachine - Modelling the behaviour of a vending machine. `NUnit, Specflow and Rhinomocks`