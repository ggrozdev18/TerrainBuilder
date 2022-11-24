using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TerrainBuilder.Contracts;
using TerrainBuilder.Data;
using TerrainBuilder.Models;

namespace TerrainBuilder.Services
{
    public class TerrainService : ITerrainService
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public double[][] Heights { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public int Octaves { get; set; }
        public double Zoom { get; set; }
        public double Power { get; set; }
        public double Influence { get; set; }

        private readonly ApplicationDbContext _context;

        public TerrainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GenerateTerrainViewModel> GenerateTerrain(int l, int w, double offX, double offY, int oct, double inf)
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
            Generate();
            GenerateTerrainViewModel viewModel = new GenerateTerrainViewModel();

            viewModel.Length = this.Length;
            viewModel.Width = this.Width;

            viewModel.Heights = new double[Length][];

            for (int i = 0; i < Length; i++)
            {
                viewModel.Heights[i] = new double[viewModel.Width];
                for (int j = 0; j < Width; j++)
                {
                    viewModel.Heights[i][j] = this.Heights[i][j];
                }
            }

            return viewModel;
        }

        public string ConvertOffset(string x)
        {
            string tempX = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '.')
                {
                    tempX += ",";
                }
                else
                {
                    tempX += x[i].ToString();
                }
            }
            return tempX;
        }

        public async Task<TerrainViewModel> CreateTerrain(TerrainViewModel tvm, string x, string y)
        {
            tvm.OffsetX = double.Parse(ConvertOffset(x));
            tvm.OffsetY = double.Parse(ConvertOffset(y));

            //ModelState.ClearValidationState(nameof(terrainViewModel));
            var validationResultList = new List<ValidationResult>();
            bool isModelValid = Validator.TryValidateObject(tvm, new ValidationContext(tvm), validationResultList);

            Terrain realTerrain = new Terrain();
            tvm.IsDBSaveSuccessful = false;

            if (isModelValid)
            {
                tvm.Id = Guid.NewGuid();
                realTerrain.Id = tvm.Id;
                realTerrain.Name = tvm.Name;
                realTerrain.DateCreated = DateTime.Now;
                realTerrain.Description = tvm.Description;
                realTerrain.Length = tvm.Length;
                realTerrain.Width = tvm.Width;
                realTerrain.OffsetX = tvm.OffsetX;
                realTerrain.OffsetY = tvm.OffsetY;
                realTerrain.Octaves = tvm.Octaves;
                realTerrain.Zoom = 1;
                realTerrain.Power = 1;
                realTerrain.Influence = tvm.Influence;
                _context.Add(realTerrain);
                await _context.SaveChangesAsync();
                tvm.IsDBSaveSuccessful = true;
            }
            return tvm;
        }

        public async Task<TerrainViewModel> EditTerrain(TerrainViewModel tvm, string x, string y)
        {
            tvm.OffsetX = double.Parse(ConvertOffset(x));
            tvm.OffsetY = double.Parse(ConvertOffset(y));

            //ModelState.ClearValidationState(nameof(terrainViewModel));
            var validationResultList = new List<ValidationResult>();
            bool isModelValid = Validator.TryValidateObject(tvm, new ValidationContext(tvm), validationResultList);

            Terrain realTerrain = await _context.Terrains.FindAsync(tvm.Id);

            tvm.IsDBSaveSuccessful = false;

            if (realTerrain == null)
            {
                return tvm;
            }

            

            if (isModelValid)
            {
               // tvm.Id = Guid.NewGuid();
               // realTerrain.Id = tvm.Id;
                realTerrain.Name = tvm.Name;
                realTerrain.DateCreated = DateTime.Now;
                realTerrain.Description = tvm.Description;
                realTerrain.Length = tvm.Length;
                realTerrain.Width = tvm.Width;
                realTerrain.OffsetX = tvm.OffsetX;
                realTerrain.OffsetY = tvm.OffsetY;
                realTerrain.Octaves = tvm.Octaves;
                realTerrain.Zoom = 1;
                realTerrain.Power = 1;
                realTerrain.Influence = tvm.Influence;
                try
                {
                    _context.Update(realTerrain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    tvm.IsDBSaveSuccessful = false;
                    return tvm;
                }
                tvm.IsDBSaveSuccessful = true;
            }
            return tvm;
        }

        public double Rand(double x)
        {
            x %= 10000;
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
                        double x = i * Zoom + OffsetX;
                        double y = j * Zoom + OffsetY;
                        Heights[i][j] += (Noise1D(double.Parse((x + Noise1D(double.Parse((j * 0.03125 + OffsetY).ToString()))).ToString())) * Power + Noise1D(double.Parse((y + Noise1D(double.Parse((i * 0.03125 + OffsetX).ToString()))).ToString())) * Power) / 2;
                    }
                }
                Zoom *= Influence;
                Power /= Influence;
            }
        }
    }
}
