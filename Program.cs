using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kraterek
{
    internal class Program
    {
        struct Adat
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double R { get; set; }
            public string Nev { get; set; }


            public Adat(string x, string y, string r, string nev)
            {
                X = double.Parse(x);
                Y = double.Parse(y);
                R = double.Parse(r);
                Nev = nev;
            }


        }



        static void Main(string[] args)
        {
            string path = @"..\..\felszin_tvesszo.txt";
            string pathki = @"..\..\terulet.txt";
            StreamReader sr = new StreamReader(path);
            List<string> sorok = new List<string>();
            List<Adat> list = new List<Adat>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] sor = line.Split('\t');
                list.Add(new Adat(sor[0], sor[1], sor[2], sor[3]));
            }
            sr.Close();
            Console.WriteLine($"2.feladat\r\nA kráterek száma: {list.Count()}");
            Console.Write("3. feladat\r\nKérem egy kráter nevét:");
            string nev = Console.ReadLine();
            var dh = (from elem in list
                      where elem.Nev == nev
                      select elem).Single();

            Console.WriteLine($"A(z) {dh.Nev} középpontja X={dh.X} Y={dh.Y} sugara R={dh.R}. ");
            var legnagyobb = (from elem in list
                              orderby elem.R descending
                              select elem).First();
            Console.WriteLine($"4. feladat\r\nA legnagyobb kráter neve és sugara: {legnagyobb.Nev} {legnagyobb.R}");

            Console.Write("6.feladat\r\n Kérem egy kráter nevét: ");
            nev = Console.ReadLine();

            var rnev = (from elem in list
                        where elem.Nev == nev
                        select elem).Single();

            double x1 = rnev.X;
            double y1 = rnev.Y;
            double R1 = rnev.R;



            var fgg = from elem in list
                      where elem.R + R1 < Math.Sqrt((elem.X - x1) * (elem.X - x1) + (elem.Y - y1) * (elem.Y - y1))
                      select elem.Nev;

            Console.WriteLine($"Nincs közös része: {string.Join(",", fgg)}");


            var rnev2 = (from elem in list
                         select elem);

            List<string> asd = new List<string>();
            Console.WriteLine("7.feladat");
            foreach (var rnev22 in list)

            {
                x1 = rnev22.X;
                y1 = rnev22.Y;
                R1 = rnev22.R;



                var fgg2 = from elem in list
                           where R1 - elem.R > Math.Sqrt((elem.X - x1) * (elem.X - x1) + (elem.Y - y1) * (elem.Y - y1))
                           select elem.Nev;
                foreach (var rnev222 in fgg2)
                {
                    asd.Add(rnev22.Nev + " " + rnev222.ToString());
                    Console.WriteLine($"A(z) {rnev22.Nev} tartalmazza a(z) {rnev222} krátert. ");
                }
            }





           
            StreamWriter sw = new StreamWriter(pathki);
            foreach (var item in list)
            {
                string ad1 = item.Nev;
                string ad2 = Math.Round((Math.Pow(item.R, 2) * 3.14), 2).ToString();
                sw.WriteLine($"{ad2}\t{ad1}");

            }
            
            sw.Close();
            Console.ReadLine();
        }
    }
}
