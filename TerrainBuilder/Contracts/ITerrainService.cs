using TerrainBuilder.Models;
using TerrainBuilder.Data;

namespace TerrainBuilder.Contracts
{
    public interface ITerrainService
    {
        Task<GenerateTerrainViewModel> GenerateTerrain(int l, int w, double offX, double offY, int oct, double inf);

        string ConvertOffset(string x);

        Task<TerrainViewModel> CreateTerrain(TerrainViewModel tvm, string x, string y, string appUserId);
        
        Task<TerrainViewModel> EditTerrain(TerrainViewModel tvm, string x, string y);

        double Rand(double x);

        double Noise1D(double x);

        void Generate();
    }
}
