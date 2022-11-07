using TerrainBuilder.Contracts;
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
    }
}
