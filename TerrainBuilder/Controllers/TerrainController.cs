using Microsoft.AspNetCore.Mvc;
using TerrainBuilder.Contracts;
using TerrainBuilder.Data;
using TerrainBuilder.Models;
using TerrainBuilder.Services;

namespace TerrainBuilder.Controllers
{
    public class TerrainController : Controller
    {
        private readonly ITerrainService terrainsv;

        public TerrainController(ITerrainService _terrainsv)
        {
            terrainsv = _terrainsv;
        }

        public async Task<IActionResult> GenerateTerrain()
        {
            GenerateTerrainViewModel terrain = new GenerateTerrainViewModel();

            terrain = await terrainsv.GenerateTerrain(150, 150, 83681.452163, 41294.53462, 5, 2);
            return View(terrain);
        }
    }
}
