This test demonstrates the issue of no valid default project config causes issues in Rosyln with project references.

The line `<Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>` in \FailingProgram\DummyProgram.csproj is causing `Compilation.References` to not include the system libraries associated with the project when no property group targets the `AnyCPU` profile.

To create a new project that fails the test:
 - Open Configuration Manager
 - Under "Active Solution Platform" chose `<New...>`
 - Create a new platform targeting `x86` with "Crate new project platforms" checked.
 - Under "Active Solution Platform" chose `<Edit...>`
 - Delete `AnyCPU`
 - Under "Platform" for DummyProgram choose `<Edit...>`
 - Delete `AnyCPU`
 
Be sure to delete the project level AnyCPU entries along with the solution level entries or the tests will not show the problem.
 
Workaround to fix the issue:
 - Edit \WorkingProgram\DummyProgram.csproj with notepad
 - Change `<Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>` to `<Platform Condition=" '$(Platform)' == '' ">x86</Platform>`
 
 
