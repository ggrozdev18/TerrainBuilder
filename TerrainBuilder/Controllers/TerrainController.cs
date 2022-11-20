using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerrainBuilder.Contracts;
using TerrainBuilder.Data;
using TerrainBuilder.Models;
using TerrainBuilder.Services;

namespace TerrainBuilder.Controllers
{
    public class TerrainController : Controller
    {
        private readonly ITerrainService terrainsv;

        private readonly ApplicationDbContext _context;

        public TerrainController(ITerrainService _terrainsv, ApplicationDbContext context)
        {
            terrainsv = _terrainsv;
            _context = context;
        }

        public async Task<IActionResult> GenerateTerrain()
        {
            GenerateTerrainViewModel terrain = new GenerateTerrainViewModel();

            terrain = await terrainsv.GenerateTerrain(150, 150, 83681.452163, 41294.53462, 5, 2);
            return View(terrain);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.TerrainViewModel.ToListAsync());
        //}

        //// GET: TerrainViewModels/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.TerrainViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var terrainViewModel = await _context.TerrainViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (terrainViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(terrainViewModel);
        //}

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
            var v = HttpContext.Request.Form["OffsetX"].ToString();
            double OffsetX = Double.Parse(v);
            terrainViewModel.OffsetX= OffsetX;

            Terrain realTerrain = new Terrain();
            if (ModelState.IsValid)
            {
                terrainViewModel.Id = Guid.NewGuid();
                realTerrain.Id = terrainViewModel.Id;
                realTerrain.Name = terrainViewModel.Name;
                realTerrain.DateCreated = DateTime.Now;
                realTerrain.Description = terrainViewModel.Description;
                realTerrain.Length = terrainViewModel.Length;
                realTerrain.Width = terrainViewModel.Width;
                realTerrain.OffsetX = terrainViewModel.OffsetX;
                realTerrain.OffsetY = terrainViewModel.OffsetY;
                realTerrain.Octaves = terrainViewModel.Octaves;
                realTerrain.Zoom = 1;
                realTerrain.Power = 1;
                realTerrain.Influence = terrainViewModel.Influence;
                _context.Add(realTerrain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(terrainViewModel);
        }

        // GET: TerrainViewModels/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.TerrainViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var terrainViewModel = await _context.TerrainViewModel.FindAsync(id);
        //    if (terrainViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(terrainViewModel);
        //}

        //// POST: TerrainViewModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Length,Width,OffsetX,OffsetY,Octaves,Influence")] TerrainViewModel terrainViewModel)
        //{
        //    if (id != terrainViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(terrainViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TerrainViewModelExists(terrainViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(terrainViewModel);
        //}

        //// GET: TerrainViewModels/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.TerrainViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var terrainViewModel = await _context.TerrainViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (terrainViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(terrainViewModel);
        //}

        //// POST: TerrainViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.TerrainViewModel == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.TerrainViewModel'  is null.");
        //    }
        //    var terrainViewModel = await _context.TerrainViewModel.FindAsync(id);
        //    if (terrainViewModel != null)
        //    {
        //        _context.TerrainViewModel.Remove(terrainViewModel);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TerrainViewModelExists(Guid id)
        //{
        //    return _context.TerrainViewModel.Any(e => e.Id == id);
        //}
    }
}
