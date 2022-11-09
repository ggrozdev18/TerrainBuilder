using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TerrainBuilder.Data;

namespace TerrainBuilder.Controllers
{
    public class MeetingStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeetingStatus
        public async Task<IActionResult> Index()
        {
              return View(await _context.MeetingStatus.ToListAsync());
        }

        // GET: MeetingStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeetingStatus == null)
            {
                return NotFound();
            }

            var meetingStatus = await _context.MeetingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingStatus == null)
            {
                return NotFound();
            }

            return View(meetingStatus);
        }

        // GET: MeetingStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MeetingStatus meetingStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingStatus);
        }

        // GET: MeetingStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeetingStatus == null)
            {
                return NotFound();
            }

            var meetingStatus = await _context.MeetingStatus.FindAsync(id);
            if (meetingStatus == null)
            {
                return NotFound();
            }
            return View(meetingStatus);
        }

        // POST: MeetingStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MeetingStatus meetingStatus)
        {
            if (id != meetingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingStatusExists(meetingStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meetingStatus);
        }

        // GET: MeetingStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetingStatus == null)
            {
                return NotFound();
            }

            var meetingStatus = await _context.MeetingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingStatus == null)
            {
                return NotFound();
            }

            return View(meetingStatus);
        }

        // POST: MeetingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeetingStatus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MeetingStatus'  is null.");
            }
            var meetingStatus = await _context.MeetingStatus.FindAsync(id);
            if (meetingStatus != null)
            {
                _context.MeetingStatus.Remove(meetingStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingStatusExists(int id)
        {
          return _context.MeetingStatus.Any(e => e.Id == id);
        }
    }
}
