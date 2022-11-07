﻿using Microsoft.AspNetCore.Mvc;
using TerrainBuilder.Contracts;
using TerrainBuilder.Models;

namespace TerrainBuilder.Controllers
{
    public class TerrainController : Controller
    {
        private readonly ITerrainService terrainsv;

        public TerrainController(ITerrainService _terrainsv)
        {
            terrainsv = _terrainsv;
        }

        public IActionResult GenerateTerrain()
        {
            return View();
        }
    }
}
