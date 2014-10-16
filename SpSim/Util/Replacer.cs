using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Util
{
    public class Replacer
    {
        public static string[] Placeholder = { "%OCA", "%OCB", "%povG", "%I", "%FI",
                                               "%G", "%C", "%R"};

        public static string Replace(string input, string[] neededParams)
        {
            for (int i = 0; i < Placeholder.Length; i++)
            {
                if (i == 0)
                {
                    input = Replace_OCA(input, neededParams[i]);
                }
                else if (i == 1)
                {
                    input = Replace_OCB(input, neededParams[i]);
                }
                else
                {
                    input = input.Replace(Placeholder[i], neededParams[i]);
                }
            }
            
            return input;
        }

        private static string Replace_OCA(string input, string girlCall)
        {
            if (girlCall != "you")
            {
                return input.Replace("%OCA", girlCall + ", ");
            }

            return input.Replace("%OCA", "");
        }

        private static string Replace_OCB(string input, string girlCall)
        {
            if (girlCall != "you")
            {
                return input.Replace("%OCB", ", " + girlCall);
            }

            return input.Replace("%OCB", "");
        }

    }
}
