using System;
using System.Text;

namespace PrecisionLenderQ2
{
    class InterleaveProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter First String:");
            string strFirst = string.Empty;
            string strSecond = string.Empty;
            string strInterleaved = string.Empty;
            strFirst = Console.ReadLine();
            Console.WriteLine("Enter Second String:");
            strSecond = Console.ReadLine();
            strInterleaved = InterleaveStrings(strFirst,strSecond);
            Console.WriteLine(strInterleaved);  
        }

      static string InterleaveStrings(string strFirst, string strSecond)
        {
            StringBuilder sbInterleaved = new StringBuilder();
            for (int j = 0; j < strFirst.Length || j < strSecond.Length; j++)
            {
                if (j < strFirst.Length)
                {
                    sbInterleaved.Append(strFirst.Substring(j,1));  
                }
                if (j < strSecond.Length)
                {
                    sbInterleaved.Append(strSecond.Substring(j,1));
                }
            }
            return sbInterleaved.ToString() ;
        }
    }

}
