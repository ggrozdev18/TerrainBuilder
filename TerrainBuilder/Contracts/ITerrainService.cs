using TerrainBuilder.Models;

namespace TerrainBuilder.Contracts
{
    public interface ITerrainService
    {
        Task<TerrainViewModel> GenerateTerrain();
    }
}
