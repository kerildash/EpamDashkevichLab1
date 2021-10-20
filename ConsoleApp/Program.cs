using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            try
            {
                List<string> inputData = GetDataFromInput();
                List<string> outputData = FormatData(inputData);
                WriteData(outputData);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private static List<string> FormatData(List<string> rawData)
        {
            List<string> formattedData = new List<string>(0);
            for(int index = 0; index<rawData.Count; index++)
            {
                string s = rawData[index];
                s = s.Replace(" ", ""); //не Trim, т. к. могут быть ещё пробелы после запятой
                if (IsNeedFormat(s))
                {
                    formattedData.Add(s.Insert(0, "X: ").Replace(",", " Y: "));
                }
                else
                {
                    throw new FormatException("Incorrect format of data.");
                }
            }
            return formattedData;
        }
        private static bool IsNeedFormat(string s)
        {
            Regex regex = new Regex(@"^\d*.\d*,\d*.\d*$"); // 2 числа с точкой через запятую, напр 23.8976,12.3218
            return regex.IsMatch(s);
        }
        private static void WriteData(List<string> strings)
        {
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }
        }

        static List<string> GetDataFromUserDialog()
        {
            Console.Write("Enter number of strings: ");
            int lenght = Convert.ToInt32(Console.ReadLine());
            var strings = new List<string>(lenght);
            for (int i = 0; i < lenght; i++)
            {
                Console.WriteLine($"Enter {i + 1}th string:");
                strings.Add(Console.ReadLine());
            }
            return strings;
        }

        static List<string> GetDataFromInput()
        {
            List<string> summaryInput = new List<string>(0);
            while(true)
            {
                string input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                summaryInput.Add(input);
                
            }
            return summaryInput;
        }

    }
}
