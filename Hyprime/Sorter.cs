using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hyprime
{
    class Sorter
    {
        public static void Sort(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                List<BigInteger> bigIntegers = new List<BigInteger>();
                int i = 0;
                foreach (string l in lines)
                {
                    string s = lines[i];
                    bigIntegers.Add(BigInteger.Parse(s));
                    i++;
                }
                bigIntegers.Sort();
                string e = string.Empty;
                foreach (BigInteger b in bigIntegers)
                    e += b.ToString() + Environment.NewLine;
                foreach (BigInteger b in bigIntegers)
                    File.WriteAllText(path, e);
            }
            catch (Exception) { }
        }
    }
}
