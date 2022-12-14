using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using TerrainBuilder.Contracts;
using TerrainBuilder.Data;
using TerrainBuilder.Models;
using TerrainBuilder.Services;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TerrainBuilder.Controllers
{
    //TODO: Clean Up Controller
    [Authorize]
    public class TerrainController : Controller
    {
        private readonly ITerrainService terrainsv;

        private readonly ApplicationDbContext _context;

        public TerrainController(ITerrainService _terrainsv, ApplicationDbContext context)
        {
            terrainsv = _terrainsv;
            _context = context;
        }

        public async Task<IActionResult> GenerateTerrain(int l, int w, string offX, string offY, int oct, double inf)
        {
            GenerateTerrainViewModel terrain = new GenerateTerrainViewModel();

            string x = terrainsv.ConvertOffset(offX);
            string y = terrainsv.ConvertOffset(offY);

            //150, 150, 83681.452163, 41294.53462, 5, 2
            terrain = await terrainsv.GenerateTerrain(l,w,double.Parse(x),double.Parse(y),oct,inf);
            return View(terrain);
        }

        public async Task<IActionResult> Index()
        {
            var userId = "";
            try
            {
                userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid user sent");
            }

            ApplicationUser appUser = await _context.Users.Where(m=>m.Id==userId).Include(m=>m.Terrains).FirstOrDefaultAsync();

            List<Terrain> allTerrains = appUser.Terrains.ToList();
            List<TerrainViewModel> tvms = new List<TerrainViewModel>();
            foreach (Terrain terrain in allTerrains) 
            {
                TerrainViewModel tvm = new TerrainViewModel();
                tvm.Id = terrain.Id;
                tvm.OffsetX = terrain.OffsetX;
                tvm.OffsetY = terrain.OffsetY;
                tvm.Width = terrain.Width;
                tvm.Length = terrain.Length;
                tvm.Influence = terrain.Influence;
                tvm.Octaves = terrain.Octaves;
                tvm.Name = terrain.Name;
                tvm.Description = terrain.Description;
                tvms.Add(tvm);
            }
            return View(tvms);
           // return View(await _context.TerrainViewModel.ToListAsync());
        }

        // GET: TerrainViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Terrains == null)
            {
                return NotFound();
            }

            var terrain = await _context.Terrains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terrain == null)
            {
                return NotFound();
            }

            TerrainViewModel tvm = new TerrainViewModel();

            tvm.Id = terrain.Id;
            tvm.Name = terrain.Name;
            tvm.Description = terrain.Description;
            tvm.Length = terrain.Length;
            tvm.Width = terrain.Width;
            tvm.OffsetX = terrain.OffsetX;
            tvm.OffsetY = terrain.OffsetY;
            tvm.Octaves = terrain.Octaves;
            tvm.Influence = terrain.Influence;

            return View(tvm);
        }

        // GET: TerrainViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TerrainViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Length,Width,OffsetX,OffsetY,Octaves,Influence")] TerrainViewModel terrainViewModel)
        {
            var userId = "";
            try
            {
                userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid user sent");
            }

            string x = HttpContext.Request.Form["OffsetX"].ToString();
            string y = HttpContext.Request.Form["OffsetY"].ToString();

            ModelState.ClearValidationState(nameof(terrainViewModel));
            TerrainViewModel tvm = await terrainsv.CreateTerrain(terrainViewModel, x, y, userId);

            if (tvm.IsDBSaveSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(tvm);
        }

        // GET: TerrainViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Terrains == null)
            {
                return NotFound();
            }

            var terrain = await _context.Terrains.FindAsync(id);
            if (terrain == null)
            {
                return NotFound();
            }

            TerrainViewModel tvm = new TerrainViewModel();
            tvm.Id = terrain.Id;
            tvm.OffsetX = terrain.OffsetX;
            tvm.OffsetY = terrain.OffsetY;
            tvm.Width = terrain.Width;
            tvm.Length = terrain.Length;
            tvm.Influence = terrain.Influence;
            tvm.Octaves = terrain.Octaves;
            tvm.Name = terrain.Name;
            tvm.Description = terrain.Description;

            return View(tvm);
        }

        //// POST: TerrainViewModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Length,Width,OffsetX,OffsetY,Octaves,Influence")] TerrainViewModel terrainViewModel)
        {

            if (id != terrainViewModel.Id)
            {
                return NotFound();
            }

            string x = HttpContext.Request.Form["OffsetX"].ToString();
            string y = HttpContext.Request.Form["OffsetY"].ToString();

            TerrainViewModel tvm = await terrainsv.EditTerrain(terrainViewModel, x, y);

            if (tvm.IsDBSaveSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(terrainViewModel);
        }

        //// GET: TerrainViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Terrains == null)
            {
                return NotFound();
            }

            var terrain = await _context.Terrains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terrain == null)
            {
                return NotFound();
            }

            TerrainViewModel tvm = new TerrainViewModel();

            tvm.Id = terrain.Id;
            tvm.Name = terrain.Name;
            tvm.Description = terrain.Description;
            tvm.Length = terrain.Length;
            tvm.Width = terrain.Width;
            tvm.OffsetX = terrain.OffsetX;
            tvm.OffsetY = terrain.OffsetY;
            tvm.Octaves = terrain.Octaves;
            tvm.Influence = terrain.Influence;

            return View(tvm);
        }

        // POST: TerrainViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Terrains == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Terrain'  is null.");
            }
            var terrain = await _context.Terrains.FindAsync(id);
            if (terrain != null)
            {
                terrain.IsDeleted = true;
                _context.Terrains.Update(terrain);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerrainViewModelExists(Guid id)
        {
            return _context.Terrains.Any(e => e.Id == id);
        }
    }
}
