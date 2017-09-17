# Katas 
Consolidating all of the code katas / tests I do so that they're easier to find. 
 
#### Restoring 
> `Update-Package -reinstall` 
 
If you have any issues with the NuGet restore run `Update-Package -reinstall` from the Package Manager Console to re-path the hintpaths in the .csproj files. 
 
Doing this will mean that Ncrunch / the projects themselves might break due to an old reference to the Microsoft.Net.Compilers package. I'll fix this one day, I swear. 
 
#### Live Db tests 
Some of the projects will require a connection string to a database as they will build, and (hopefully), destroy a database to run a full end-to-end test. Should always be mentioned in the list below. 
 
# List 
 
* CustomerManagement - A simple customer manager where you get insert, update and get customers. `Owin Hosted WebApi featuring NUnit, Specflow with Db tests and Rhinomocks` 
* RoverExercise - An agent that moves over a grid, following commands. `Client library featuring NUnit, Specflow and Rhinomocks`
* TennisScores - Reads Tennis scores from a file and outputs them. `Client library featuring NUnit, FluentAssertions and NSubstitute`
* VendingMachine - Modelling the behaviour of a vending machine. `Client library featuring NUnit, Specflow and Rhinomocks`