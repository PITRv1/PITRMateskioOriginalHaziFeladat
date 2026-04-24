using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Signal(string name, int hour, int minute, int speed) {
            this.Name = name;
            this.Hour = hour;
            this.Minute = minute;
            this.Speed = speed;
        }
    }
    internal class Program
    {
        public static List<Signal> signals = new List<Signal>();
        static void Main(string[] args) {
            signals = GetData("jeladas.txt");

            Console.WriteLine("2. feladat:");
            Signal last = signals[signals.Count - 1];
            Console.WriteLine("Az utolso jeladas idopontja " + last.Hour + ":" + last.Minute + ", a jarmu rendszama " + last.Name);

            Console.WriteLine("\n3. feladat:");
            string firstGuyName = signals[0].Name;
            List<Signal> firstCarSignals = signals.Where(x => x.Name == firstGuyName).ToList();
            Console.WriteLine("Az elso jarmu: " + firstGuyName);
            Console.Write("Jeladasainak idopontjai: ");
            Console.WriteLine(string.Join(" ", firstCarSignals.Select(x => x.Hour + ":" + x.Minute)));

            Console.WriteLine("\n4. feladat:");
            Console.Write("Kerem, adja meg az orat: ");
            int hourInput = int.Parse(Console.ReadLine());
            Console.Write("Kerem, adja meg a percet: ");
            int minuteInput = int.Parse(Console.ReadLine());
            int count = signals.Count(x => x.Hour == hourInput && x.Minute == minuteInput);
            Console.WriteLine("A jeladasok szama: " + count);

            Console.WriteLine("\n5. feladat:");
            int highestSpeed = signals.Max(x => x.Speed);
            List<Signal> fastest = signals.Where(x => x.Speed == highestSpeed).ToList();
            Console.WriteLine("A legnagyobb sebesseg km/h: " + highestSpeed);
            Console.WriteLine("A jarmuvek: " + string.Join(" ", fastest.Select(x => x.Name)));

            Console.WriteLine("\n6. feladat:");
            Console.Write("Kerem, adja meg a rendszamot: ");
            string plateInput = Console.ReadLine();
            List<Signal> carSignals = signals.Where(x => x.Name == plateInput).ToList();
            if (carSignals.Count == 0) {
                Console.WriteLine("Nem szerepel ilyen rendszamu jarmu.");
            } else {
                double distance = 0.0;
                for (int i = 0; i < carSignals.Count; i++) {
                    Console.WriteLine(carSignals[i].Hour + ":" + carSignals[i].Minute + " " + distance.ToString("F1") + " km");
                    if (i < carSignals.Count - 1) {
                        int currentMinutes = carSignals[i].Hour * 60 + carSignals[i].Minute;
                        int nextMinutes = carSignals[i + 1].Hour * 60 + carSignals[i + 1].Minute;
                        double elapsedHours = (nextMinutes - currentMinutes) / 60.0;
                        distance += carSignals[i].Speed * elapsedHours;
                    }
                }
            }

            Console.WriteLine("\n7. feladat:");
            List<string> carNames = signals.Select(x => x.Name).Distinct().ToList();
            StreamWriter sw = new StreamWriter("ido.txt");
            foreach (string name in carNames) {
                List<Signal> carGroup = signals.Where(x => x.Name == name).ToList();
                Signal first = carGroup.First();
                Signal lastSig = carGroup.Last();
                sw.WriteLine(name + " " + first.Hour + " " + first.Minute + " " + lastSig.Hour + " " + lastSig.Minute);
            }
            sw.Close();
            Console.WriteLine("Az ido.txt allomany elkeszult.");
        }
        public static List<Signal> GetData(string path) {
            List<Signal> result = new List<Signal>();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream) {
                string[] row = sr.ReadLine().Replace("\t", " ").Split(' ');
                Signal newSignal = new Signal(row[0], int.Parse(row[1]), int.Parse(row[2]), int.Parse(row[3]));
                result.Add(newSignal);
            }
            return result;
        }
    }
}