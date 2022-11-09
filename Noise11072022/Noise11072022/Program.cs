using System.Security.Cryptography;

namespace Noise11072022
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Console.Clear();
            Terrain t = new Terrain(100, 100, 373.456, 494.562, 5, 2);
            //Good Seeds
            //743.456, 894.562
            //753.456, 904.562
            //1395.3256, 714.6814
            //1395.3256, 1114.6884
            //1395.3256, 1014.6884
            //373.456, 494.562
            t.Generate();
            t.Print();
        }
    }

    public class Terrain
    {
        public Terrain(int l, int w, double offX, double offY, int oct, double inf)
        {
            Length = l;
            Width = w;
            OffsetX = offX;
            OffsetY = offY;
            Octaves = oct;
            Zoom = 1;
            Power = 1;
            Influence = inf;
            Heights = new double[Length][];
            for (int i = 0; i < l; i++)
            {
                Heights[i] = new double[Width];
            }
        }

        public int Length { get; set; }

        public int Width { get; set; }

        public double[][] Heights { get; set; }

        public double OffsetX { get; set; }
        
        public double OffsetY { get; set; }

        public int Octaves { get; set; }

        public double Zoom { get; set; }

        public double Power { get; set; }

        public double Influence { get; set; }

        public double Rand(double x) 
        {
            x %= 1000;
            int counter = 1;
            int[] primes = new int[100];

            for (int i = 2; i < 100; i++)
            {
                if (primes[i] == 0)
                {
                    if (counter % 7 == 0)
                    {
                        x %= i;
                    }
                    else if (counter % 7 == 1 || counter % 7 == 2 || counter % 7 == 3)
                    {
                        x *= i;
                    }
                    else 
                    {
                        x /= i;
                    }
                    for (int j = i; j < 100; j += i)
                    {
                        primes[j] = i;
                    }
                    counter++;
                }
            }
            
            return x % 2;
        }

        public double Noise1D(double x) 
        {
            double b = 0;
            double t = 0;
            
            if (x % 1 != 0)
            {
                b = Rand(Math.Floor(x));
                t = Rand(Math.Ceiling(x));
            }
            else 
            {
                if (x > 1)
                {
                    b = Rand(x - 1);
                }
                t = Rand(x + 1);
            }
            
            double inc = 0;
            double min = 0;
            double d = x - b;

            if (x % 1 != 0)
            {
                d = x - Math.Floor(x);
            }
            else
            {
                if (x == 1)
                {
                    d = 1;
                }
                else 
                {
                    d = 0;
                }
            }

            if (b > t)
            {
                min = t;
                inc = b - t;
                return (-3 * d * d + 2 * d * d * d + 1) * inc + min - 1;
            }
            else 
            {
                min = b;
                inc = t - b;
                return (3 * d * d - 2 * d * d * d) * inc + min - 1;
            }
        }

        public void Generate() 
        {
            for (int i = 0; i < Octaves; i++)
            {
                Zoom /= Influence;
            }
            for (int k = 0; k < Octaves; k++)
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Heights[i][j] += (Noise1D(double.Parse((i * Zoom + OffsetX).ToString())) * Power + Noise1D(double.Parse((j * Zoom + OffsetY).ToString())) * Power) / 2;
                    }
                }
                Zoom *= Influence;
                Power /= Influence;
            }
        }

        public void ColorCode(double x) 
        {
            if (x < -0.8)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else if (x >= -0.8 && x < -0.5)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            else if (x >= -0.5 && x < -0.3)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (x >= -0.3 && x < -0.1)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (x >= -0.1 && x < 0.2)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            else if (x >= 0.2 && x < 0.6)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else 
            {
                Console.BackgroundColor = ConsoleColor.White;
            }

        }

        public void Print() 
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Heights[i][j] >= -0.4)
                    {
                        Random r = new Random();
                        if (r.Next(175) == 25)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("  ");
                        }
                        else
                        {
                            ColorCode(Heights[i][j]);
                            Console.Write("  ");
                        }
                    }
                    else 
                    {
                        ColorCode(Heights[i][j]);
                        Console.Write("  ");
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
    }
}