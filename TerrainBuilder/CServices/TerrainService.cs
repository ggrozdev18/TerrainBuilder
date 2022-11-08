﻿using TerrainBuilder.Contracts;
using TerrainBuilder.Models;

namespace TerrainBuilder.Services
{
    public class TerrainService : ITerrainService
    {
        public async Task<TerrainViewModel> GenerateTerrain()
        {
            TerrainViewModel viewModel = new TerrainViewModel();
            return viewModel;
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
    }
}