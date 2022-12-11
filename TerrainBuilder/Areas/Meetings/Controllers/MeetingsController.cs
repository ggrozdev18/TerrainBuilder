using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TerrainBuilder.Data;

namespace TerrainBuilder.Areas.Meetings.Controllers
{
    [Area("Meetings")]
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meetings/Meetings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Meetings.ToListAsync());
        }

        // GET: Meetings/Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Meetings/Create
        public IActionResult Create()
        {
            Meeting m = new Meeting();
            m.MeetingStatuses = GetMeetingStatuses();
            m.MeetingTypes = GetMeetingTypes();
            m.Cities = GetCities();
            return View(m);
        }

        // POST: Meetings/Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meeting meeting)
        {
            meeting.MeetingStatuses = GetMeetingStatuses();
            meeting.MeetingTypes = GetMeetingTypes();
            meeting.Cities = GetCities();

            ModelState.ClearValidationState(nameof(meeting));

            meeting.MeetingStatus = await _context.MeetingStatuses.FindAsync(meeting.MeetingStatusId);
            meeting.MeetingType = await _context.MeetingTypes.FindAsync(meeting.MeetingTypeId);
            meeting.City = await _context.Cities.FindAsync(meeting.CityId);

            var validationResultsList = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(meeting, new ValidationContext(meeting), validationResultsList);

            if (isValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            meeting.MeetingStatuses = GetMeetingStatuses();
            meeting.MeetingTypes = GetMeetingTypes();
            meeting.Cities = GetCities();

            return View(meeting);
        }

        // POST: Meetings/Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            meeting.MeetingStatuses = GetMeetingStatuses();
            meeting.MeetingTypes = GetMeetingTypes();
            meeting.Cities = GetCities();

            ModelState.ClearValidationState(nameof(meeting));

            meeting.MeetingStatus = await _context.MeetingStatuses.FindAsync(meeting.MeetingStatusId);
            meeting.MeetingType = await _context.MeetingTypes.FindAsync(meeting.MeetingTypeId);
            meeting.City = await _context.Cities.FindAsync(meeting.CityId);

            var validationResultsList = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(meeting, new ValidationContext(meeting), validationResultsList);

            if (isValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            return View(meeting);
        }

        // GET: Meetings/Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meetings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Meetings'  is null.");
            }
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
          return _context.Meetings.Any(e => e.Id == id);
        }

        private List<SelectListItem> GetMeetingTypes() 
        {
            List<SelectListItem> meetingTypes = new List<SelectListItem>();
            var _meetingTypes = _context.MeetingTypes;
            foreach (var item in _meetingTypes) 
            {
                meetingTypes.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString()});
            }
            return meetingTypes;
        }

        private List<SelectListItem> GetMeetingStatuses()
        {
            List<SelectListItem> meetingStatuses = new List<SelectListItem>();
            var _meetingStatuses = _context.MeetingStatuses;
            foreach (var item in _meetingStatuses)
            {
                meetingStatuses.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return meetingStatuses;
        }

        private List<SelectListItem> GetCities()
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            var _cities = _context.Cities;
            foreach (var item in _cities)
            {
                cities.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return cities;
        }
    }
}
