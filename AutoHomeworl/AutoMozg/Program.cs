using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AutoMozg
{
    public struct Signal
    {
        public string Name { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Speed { get; private set; }

        public Signal(string name, int hour, int minute, int speed)
        {
            this.Name = name;
            this.Hour = hour;
            this.Minute = minute;
            this.Speed = speed;
        }
    }

    internal class Program
    {
        public static List<Signal> signals = new List<Signal>();

        static void Main(string[] args)
        {
            signals = GetData("jeladas.txt");

            Console.WriteLine("1. feladat:");
            Console.WriteLine("name. "+ signals[signals.Count - 1].Name + " hour: " + signals[signals.Count - 1].Hour + " minute: " + signals[signals.Count - 1].Minute);

            Console.WriteLine("2. feladat:");

            string firstGuyName = signals[0].Name;
            // Find all the other entries with that guys name using Linq
            List<Signal> entries = signals.Where(x => x.Name == firstGuyName).ToList();
            Console.WriteLine("");

            Console.Write("Input Hours >> ");
            string hourInput = Console.ReadLine();

            Console.Write("Input Minutes >> ");
            string minuteInput = Console.ReadLine();
            entries = signals.Where(x => x.Hour == int.Parse(hourInput) && x.Minute == int.Parse(minuteInput)).ToList();
            Console.WriteLine("");

            int highestSpeed = signals.Max(x => x.Speed);
            entries = signals.Where(x => x.Speed == highestSpeed).ToList();
            Console.WriteLine("");

            

        }

        public static List<Signal> GetData(string path)
        {
            List<Signal> result = new List<Signal>();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Replace("\t", " ").Split(' ');
                Signal newSignal = new Signal(row[0], int.Parse(row[1]), int.Parse(row[2]), int.Parse(row[3]));
                result.Add(newSignal);
            }

            return result;

        }
    }
}
