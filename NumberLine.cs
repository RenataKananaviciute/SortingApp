using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SortingApp
{
    public class NumberLine
    {
        public static int[] GetNumbersArray(string line)
        {
            int[] numbersarray = line.Split(' ').Select(int.Parse).ToArray();
            return numbersarray;
        }

        public static double BubleSort(int[] numbersarray)
        {
            int maxvalue;
            DateTime start, stop;
            start = DateTime.UtcNow;
            for (int i = 0; i < numbersarray.Length-1; i++)
            {
                for (int j = 0; j < (numbersarray.Length - 1)-i; j++)
                {
                    if (numbersarray[j]>numbersarray[j+1])
                    {
                        maxvalue = numbersarray[j];
                        numbersarray[j] = numbersarray[j + 1];
                        numbersarray[j + 1] = maxvalue;
                    }
                }
            }
            stop = DateTime.UtcNow;
            return (stop - start).TotalMilliseconds;
        }
        public static double SelectionSort(int[] numbersarray)
        {
            int minvalueindex = 0;
            int tmpvalue = 0;
            DateTime start, stop;
            start = DateTime.UtcNow;
            for (int i = 0; i < numbersarray.Length - 1; i++)
            {
               minvalueindex = i;     
               for (int j = i+1; j < numbersarray.Length - 1; j++)
                {
                    if (numbersarray[j] < numbersarray[minvalueindex])
                    {
                        minvalueindex = j;
                    }                 
                }
                if (minvalueindex != i)
                {
                    tmpvalue = numbersarray[i];
                    numbersarray[i] = numbersarray[minvalueindex];
                    numbersarray[minvalueindex] = tmpvalue;
                }
            }
            stop = DateTime.UtcNow;
            return (stop - start).TotalMilliseconds;
        }
        public static string WritetoFile(string initialline, string bubbleline, string selectionline)
        {
            string filetext =initialline + "\n" + bubbleline + "\n " + selectionline;               
            string filepath = Path.Combine(Path.GetTempPath(), "result.txt");
            File.WriteAllText(filepath, filetext);
            return filepath;
        }
        public static string ReadFile(string filepath)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");
            return text;
        }
        
    }
}
