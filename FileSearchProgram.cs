using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecisionLenderQ2
{
    class FileSearchProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Path:");
            string strPath = string.Empty;
            string strSearch = string.Empty;
            string strOutput = string.Empty;
            string strResults = string.Empty;
            strPath = Console.ReadLine();
            Console.WriteLine("Enter Search String:");
            strSearch = Console.ReadLine();
            Console.WriteLine("Enter Output Filename:");
            strOutput = Console.ReadLine();
            strResults = FindFileString(strPath, strSearch, strOutput);
            Console.WriteLine(strResults);  
        }

      static string FindFileString(string strPath, string strSearch, string strOutput)
        {
            try
            {
                var files= from file in Directory.EnumerateFiles(strPath, "*.txt", SearchOption.AllDirectories) 
                            from line in File.ReadLines(file)
                            where line.Contains(strSearch)
                            select new
                            {
                                File = file,
                                Line = line
                            };
                // Grab distinct files and count them
                int Count_File = files.Select(x => x.File).Distinct().Count();
                //initialize my line / occurence counters
                int Count_Line = 0;
                int Count_Occurences = 0;
                List<string> LineItems = new List<string>();
                Parallel.ForEach(files, (file) =>
                {
                    //Counter for total occurences
                    int a = 0;
                    while ((a = file.Line.IndexOf(strSearch, a)) != -1)
                    {
                        a += strSearch.Length;
                        Count_Occurences++;
                    }
                    //Add line item and count the line item
                    LineItems.Add(file.Line);  
                    Count_Line++; 

                });
                var strOutputFile = strPath + strOutput + ".txt";
                if (!File.Exists(strOutputFile))
                {
                    // Create a file to write to
                    using (StreamWriter sw = File.AppendText(strOutputFile))
                    {
                        foreach (string item in LineItems)
                        {
                            sw.WriteLine(item);
                        }
                        sw.WriteLine("Total Number of Occurrences: " + Count_Occurences);
                        sw.WriteLine("Files Processed: " + Count_File);
                        sw.WriteLine("Number of lines found: " + Count_Line);
                        sw.Close(); 
                    }
                    
                }
            }
            catch (UnauthorizedAccessException uAEx)
            {
                return (uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                return (pathEx.Message);
            }
            return "Sucess";
          
        }
    }
}
