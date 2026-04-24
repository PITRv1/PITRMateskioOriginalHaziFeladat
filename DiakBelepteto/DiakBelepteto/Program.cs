using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiakBelepteto
{
    public struct Entry
    {
        public string Code { get; private set; }
        public string Time { get; private set; }
        public int EventCode { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }

        public Entry(string code, string time, int eventCode) {
            this.Code = code;
            this.Time = time;
            this.EventCode = eventCode;
            this.Hour = int.Parse(time.Split(':')[0]);
            this.Minute = int.Parse(time.Split(':')[1]);
        }
    }

    internal class Program
    {
        public static List<Entry> entries = new List<Entry>();

        static void Main(string[] args) {
            entries = GetData("bedat.txt");

            Console.WriteLine("2. feladat");
            Entry firstEntry = entries.First(x => x.EventCode == 1);
            Entry lastExit = entries.Last(x => x.EventCode == 2);
            Console.WriteLine("Az elso tanulo " + firstEntry.Time + "-kor lepett be a fokapun.");
            Console.WriteLine("Az utolso tanulo " + lastExit.Time + "-kor lepett ki a fokapun.");

            Console.WriteLine("\n3. feladat");
            var lateEntries = entries.Where(x =>
                x.EventCode == 1 &&
                (x.Hour > 7 || (x.Hour == 7 && x.Minute > 50)) &&
                (x.Hour < 8 || (x.Hour == 8 && x.Minute <= 15))
            ).ToList();
            StreamWriter sw = new StreamWriter("kesok.txt");
            foreach (Entry e in lateEntries) {
                sw.WriteLine(e.Time + " " + e.Code);
            }
            sw.Close();
            Console.WriteLine("A kesok.txt allomany elkeszult.");

            Console.WriteLine("\n4. feladat");
            int lunchCount = entries.Count(x => x.EventCode == 3);
            Console.WriteLine("A menzan aznap " + lunchCount + " tanulo ebedelt.");

            Console.WriteLine("\n5. feladat");
            int libraryCount = entries.Where(x => x.EventCode == 4).Select(x => x.Code).Distinct().Count();
            Console.WriteLine("Aznap " + libraryCount + " tanulo kolcsonzott a konyvtarban.");
            if (libraryCount > lunchCount)
                Console.WriteLine("Tobben voltak, mint a menzan.");
            else
                Console.WriteLine("Nem voltak tobben, mint a menzan.");

            Console.WriteLine("\n6. feladat");
            var breakStart = new { Hour = 10, Minute = 45 };
            var backDoorClose = new { Hour = 10, Minute = 50 };

            var presentAtBreak = new HashSet<string>();
            foreach (Entry e in entries) {
                if (e.Hour * 60 + e.Minute <= breakStart.Hour * 60 + breakStart.Minute) {
                    if (e.EventCode == 1) presentAtBreak.Add(e.Code);
                    else if (e.EventCode == 2) presentAtBreak.Remove(e.Code);
                }
            }

            var exitedDuringBreak = new HashSet<string>();
            foreach (Entry e in entries) {
                int t = e.Hour * 60 + e.Minute;
                int tBreak = breakStart.Hour * 60 + breakStart.Minute;
                int tClose = backDoorClose.Hour * 60 + backDoorClose.Minute;
                if (e.EventCode == 2 && t >= tBreak && t <= tClose && presentAtBreak.Contains(e.Code)) {
                    exitedDuringBreak.Add(e.Code);
                }
            }

            var returnedAfterClose = new HashSet<string>();
            foreach (Entry e in entries) {
                int t = e.Hour * 60 + e.Minute;
                int tClose = backDoorClose.Hour * 60 + backDoorClose.Minute;
                if (e.EventCode == 1 && t > tClose && exitedDuringBreak.Contains(e.Code)) {
                    returnedAfterClose.Add(e.Code);
                }
            }

            Console.WriteLine("Az erintett tanulok:");
            Console.WriteLine(string.Join(" ", returnedAfterClose));

            Console.WriteLine("\n7. feladat");
            Console.Write("Egy tanulo azonositoja=");
            string inputCode = Console.ReadLine();
            List<Entry> studentEntries = entries.Where(x => x.Code == inputCode).ToList();
            if (studentEntries.Count == 0) {
                Console.WriteLine("Ilyen azonositoju tanulo aznap nem volt az iskolaban.");
            } else {
                Entry firstIn = studentEntries.First(x => x.EventCode == 1);
                Entry lastOut = studentEntries.Last(x => x.EventCode == 2);
                int totalMinutes = (lastOut.Hour * 60 + lastOut.Minute) - (firstIn.Hour * 60 + firstIn.Minute);
                int hours = totalMinutes / 60;
                int minutes = totalMinutes % 60;
                Console.WriteLine("A tanulo erkezese es tavozasa kozott " + hours + " ora " + minutes + " perc telt el.");
            }
        }

        public static List<Entry> GetData(string path) {
            List<Entry> result = new List<Entry>();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream) {
                string[] row = sr.ReadLine().Split(' ');
                Entry newEntry = new Entry(row[0], row[1], int.Parse(row[2]));
                result.Add(newEntry);
            }
            return result;
        }
    }
}