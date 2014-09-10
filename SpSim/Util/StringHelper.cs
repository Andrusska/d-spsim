using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpSim.Util
{
    public class StringHelper
    {
        public static string UnbreakLines(string input)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);  

            input = input.Replace("\r\n", "");
            input = input.Replace("\t", "");
            return regex.Replace(input, @" ");
        }
    }
}
