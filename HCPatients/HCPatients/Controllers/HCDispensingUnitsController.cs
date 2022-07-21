using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HCPatients.Models;

namespace HCPatients.Controllers
{
    public class HCDispensingUnitsController : Controller
    {
        private readonly HCPatientsContext _context;

        public HCDispensingUnitsController(HCPatientsContext context)
        {
            _context = context;
        }

        // GET: HCDispensingUnits
        // This method redirect to DispensingUnits
        public async Task<IActionResult> Index()
        {
              return _context.DispensingUnits != null ? 
                          View(await _context.DispensingUnits.ToListAsync()) :
                          Problem("Entity set 'HCPatientsContext.DispensingUnits'  is null.");
        }

        // GET: HCDispensingUnits/Details/5
        // This method redirect to the Details Page of DispensingUnits
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // GET: HCDispensingUnits/Create
        // This method redirected to DispensingUnits Create page
        public IActionResult Create()
        {
            return View();
        }

        // POST: HCDispensingUnits/Create
        // This method handles all the actions on the Create Page of DispensingUnits.On this page user can create the records and it will be reflected on database
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispensingUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispensingUnit);
        }

        // GET: HCDispensingUnits/Edit/5
        // This method redirects to the Edit page of DispensingUnits. On this page, user can edit any record
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits.FindAsync(id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: HCDispensingUnits/Edit/5
        // This page will handle all the actions of Edit page i.e. any change on this page will be handled by this function
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            if (id != dispensingUnit.DispensingCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispensingUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispensingUnitExists(dispensingUnit.DispensingCode))
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
            return View(dispensingUnit);
        }

        // GET: HCDispensingUnits/Delete/5
        // This method redirects to the Delete page of DispensingUnits. On this page, user can delete any record
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // POST: HCDispensingUnits/Delete/5
        // This method handle all the actions on Delete Page of DispensingUnits and it is also reflectd on the page if any action done
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DispensingUnits == null)
            {
                return Problem("Entity set 'HCPatientsContext.DispensingUnits'  is null.");
            }
            var dispensingUnit = await _context.DispensingUnits.FindAsync(id);
            if (dispensingUnit != null)
            {
                _context.DispensingUnits.Remove(dispensingUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DispensingUnitExists(string id)
        {
          return (_context.DispensingUnits?.Any(e => e.DispensingCode == id)).GetValueOrDefault();
        }
    }
}
