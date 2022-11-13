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
            TerrainViewModel terrain = new TerrainViewModel();

            terrain = await terrainsv.GenerateTerrain(500, 500, 373.456, 494.562, 5, 2);
            return View(terrain);
        }
    }
}
