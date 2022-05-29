## Hotel Management System For Peak Account Interview
### C# Console Application develop on Vistual Studio 2022

### Information !!

In Project include input and output data as text file.

In main program will read every line of text file and select command to display result.

This project is not connect to databases.


### How to test !!??
This project also include UnitTest solution.

File ```TestMain.cs``` has function ``Compare_Input_Output_File_Should_Equal_In_Every_Lines`` that can run every lines of `input.txt` and compare result with `output.txt`


### Issues that you may found

if `System.IO` not found files `input.txt` or `output.txt` 
- Step 1 Right click `.txt` file and click `Properties`
- Step 2 Set `Build Action` as `Content`
- Step 3 Set `Copy to Output Directory` as `Copy always`
- Step 4 Run Program (or Test)
