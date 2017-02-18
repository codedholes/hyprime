using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hyprime
{
    class Program
    {
        static string saveName = "p.txt", lastChecked = "l.txt";
        static string saveQue;
        private static BigInteger b = 0;
        static List<BigInteger> powers = new List<BigInteger>();

        private static IEnumerable<bool> Infinite()
        {
            while (true)
                yield return true;
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
                saveName = args[0].Contains(".txt") ? args[0] : args[0] + ".txt";
            powers.Capacity = 111474836;
            while (true)
            {
                BigInteger i = 0;
                Console.Clear();
                Console.WriteLine("Writing findings to " + saveName);
                if (File.Exists(saveName))
                {
                    try
                    {
                        string[] contents = File.ReadAllLines(lastChecked);
                        i = BigInteger.Parse(contents[0]);
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(1000);
                Thread w = new Thread(() => SaveFindings());
                w.Start();
                Parallel.ForEach(Infinite(), new ParallelOptions(), f =>
                {
                    i++;
                    if (isPrimeMersenne(i) || !isPrimeMersenne(i))
                        b = i;
                });
            }
        }

        public static void GUI()
        {
            while (true)
            {
                int ct = Console.CursorTop, cl = Console.CursorLeft;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Found {0} primes in {1}.");
            }
        }

        public static BigInteger Pow(BigInteger value, BigInteger exponent)
        {
            BigInteger originalValue = value;
            while (exponent-- > 1)
                value = BigInteger.Multiply(value, originalValue);
            return value;
        }

        public static bool isPrimeMersenne(BigInteger p)
        {
            BigInteger s = 4;
            BigInteger M = Pow(2, p) - 1;

            for (BigInteger i = 0; i < p; i++)
            {
                s = ((s * s) - 2) % M;
                if (s == 0 && !powers.Contains(p))
                { powers.Add(p); Console.WriteLine("FOUND N^{0}-1 IS PRIME", p); saveQue += p + Environment.NewLine; return true; }
            }
            return false;
        }

        public static void SaveFindings()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(5000);
                    Sorter.Sort(saveName);
                    Thread.Sleep(5000);
                    File.AppendAllText(saveName, saveQue);
                    File.Delete(lastChecked);
                    File.AppendAllText(lastChecked, b.ToString());
                    saveQue = string.Empty;
                }
                catch (Exception e)
                {
                    Console.WriteLine("SAVING FAILED!");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}