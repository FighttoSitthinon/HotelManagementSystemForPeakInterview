using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelManagementSystemForPeakInterview.Test
{
    public class TestMain
    {
        private readonly Main main;
        public TestMain()
        {
            main = new Main();
        }

        [Fact]
        public void Compare_Input_Output_File_Should_Equal_In_Every_Lines()
        {
            string _filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
           
            string[] inputLines = File.ReadAllLines(_filePath + "/TestData/Input.txt");
            string[] outputLines = File.ReadAllLines(_filePath + "/TestData/Output.txt");

            for (int x = 0; x < inputLines.Length; x++)
            {
                var result = main.ExtractString(inputLines[x]);
                Console.WriteLine(result);

                var expectResult = outputLines[x];
                Console.WriteLine(expectResult);

                Assert.Equal(expectResult, result);
            }
        }

    }
}
