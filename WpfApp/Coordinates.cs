using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// Contains a property which keeps coordinates and methods for work with coordinates.
    /// </summary>
    public class Coordinates
    {
        private string[] _CoordsData;

        /// <summary>
        /// Coordinates as massive of strings
        /// </summary>
        public string[] CoordsData
        {
            get
            {
                return _CoordsData;
            }
            set
            {
                _CoordsData = value;
            }
        }
        
        /// <summary>
        /// Formats coordinates to make it easier to read.
        /// </summary>
        /// <param name="rawData">Array of coordinates in required format.</param>
        /// <returns>Array of formatted coordinates.</returns>
        public string[] FormatData(string[] rawData)
        {
            var formattedData = new string[rawData.Length];
            for (int index = 0; index < rawData.Length; index++)
            {
                string s = rawData[index];
                s = s.Replace(" ", ""); 
                if (IsNeedFormat(s))
                {
                    formattedData[index] = s.Insert(0, "X: ").Replace(",", " Y: ");
                }
                else
                {
                    throw new FormatException("Incorrect format of data.");
                }
            }
            return formattedData;
        }
        private bool IsNeedFormat(string s)
        {
            Regex regex = new Regex(@"^\d*.\d*,\d*.\d*$"); 
            return regex.IsMatch(s);
        }
        /// <summary>
        /// Writes data to file
        /// </summary>
        /// <param name="strings">Array of coordinates.</param>
        /// <param name="pathOfOutputFile">Path to a file where data should be stored.</param>
        public void WriteToFile(string[] strings, string pathOfOutputFile)
        {
            var builder = new StringBuilder();
            foreach (string str in strings)
            {
                builder.Append(str);
                builder.Append("\n");
            }
            string outputString = builder.ToString();
            File.WriteAllText(pathOfOutputFile, outputString);
        }
        /// <summary>
        /// Read coordinates from file as an array of strings.
        /// </summary>
        /// <param name="pathOfFile">Path to a file from which data should be read.</param>
        /// <returns>Array of coordinates.</returns>
        public string[] GetStringsFromFile(string pathOfFile)
        {
            return File.ReadAllLines(pathOfFile);
        }
    }
}
