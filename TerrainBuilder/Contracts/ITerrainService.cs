using TerrainBuilder.Models;

namespace TerrainBuilder.Contracts
{
    public interface ITerrainService
    {
        Task<GenerateTerrainViewModel> GenerateTerrain(int l, int w, double offX, double offY, int oct, double inf); 
        
        double Rand(double x);

        double Noise1D(double x);

        void Generate();
    }
}
