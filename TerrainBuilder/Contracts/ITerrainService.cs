using TerrainBuilder.Models;

namespace TerrainBuilder.Contracts
{
    public interface ITerrainService
    {
        Task<TerrainViewModel> GenerateTerrain(int l, int w, double offX, double offY, int oct, double inf);
    }
}
