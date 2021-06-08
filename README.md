#Code Skills Challenge

##Dependencies
.NET Core 5.0

#Building
##From IDE
open the .sln in your favourite .net ide and build the solution from there.

## From CLI
 
from the root directory `dotnet build `

# Tests
## From IDE
open the .sln in your favourite .net ide
1. run nuget restore
2. build the testing project
3. run the tests (right click -> run tests)


## From CLI
from the root directory
`dotnet test`

# Running
## From IDE


1. open the .sln in your favourite .net ide
2. Open bunnings project, then the Inputs folder, and modify/edit/add input files.
3. modify your run configurations to something like `barcodesA.csv barcodesB.csv catalogA.csv catalogB.csv`
4. run the project
5. output should be at `\Bunnings\bin\Debug\net5.0\output.csv`  you can confirm output file location from the message in the console.

## From CLI
ensure the project has been built.
1. modify csv's in Inputs folder
2. from your terminal `cd Bunnings`
3. from your terminal `dotnet run barcodesA.csv barcodesB.csv catalogA.csv catalogB.csv`
4. open `outpout.csv` from current folder. you can confirm output file location from the message in the console. 


# Assumptions
1. SKU in barcodes.csv are present in catalog.sv
2. the order of the output rows does not matter
3. Input files are supplied in order: barcodesA.csv barcodesB.csv catalogA.csv catalogB.csv
4. output file is called output.csv


# Future improvements
1. Support for more than just 2 companies.
2. GetDuplicateSupplierProductBarcodesFromCompanyB can be a private method, and tests removed

