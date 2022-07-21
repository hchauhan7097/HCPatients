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
    public class HCConcentrationUnitsController : Controller
    {
        private readonly HCPatientsContext _context;

        public HCConcentrationUnitsController(HCPatientsContext context)
        {
            _context = context;
        }

        // GET: HCConcentrationUnits
        // This method redirect to ConcentrationUnits 
        public async Task<IActionResult> Index()
        {
              return _context.ConcentrationUnits != null ? 
                          View(await _context.ConcentrationUnits.ToListAsync()) :
                          Problem("Entity set 'HCPatientsContext.ConcentrationUnits'  is null.");
        }

        // GET: HCConcentrationUnits/Details/5
        // This method redirect to the Details Page of ConcentrationUnits
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // GET: HCConcentrationUnits/Create
        // This method redirected to ConcentrationUnits Create page
        public IActionResult Create()
        {
            return View();
        }

        // POST: HCConcentrationUnits/Create
        // This method handles all the actions on the Create Page of ConcentrationUnits.On this page user can create the records and it will be reflected on database
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concentrationUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concentrationUnit);
        }

        // GET: HCConcentrationUnits/Edit/5
        // This method redirects to the Edit page of ConcentrationUnits. On this page, user can edit any record
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            return View(concentrationUnit);
        }

        // POST: HCConcentrationUnits/Edit/5
        // This page will handle all the actions of Edit page i.e. any change on this page will be handled by this function
        /// <param name="id"></param>
        /// <param name="concentrationUnit"></param>
        /// <returns></returns>etails, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (id != concentrationUnit.ConcentrationCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concentrationUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcentrationUnitExists(concentrationUnit.ConcentrationCode))
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
            return View(concentrationUnit);
        }

        // GET: HCConcentrationUnits/Delete/5
        // This method redirects to the Delete page of ConcentrationUnits. On this page, user can delete any record
        // GET method for Delete page of HCCOncentrationUnits
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // POST: HCConcentrationUnits/Delete/5
        // This method handle all the actions on Delete Page of concentrationUnits and it is also reflectd on the page if any action done
        // POST method for Delete page of HCCOncentrationUnits
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ConcentrationUnits == null)
            {
                return Problem("Entity set 'HCPatientsContext.ConcentrationUnits'  is null.");
            }
            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit != null)
            {
                _context.ConcentrationUnits.Remove(concentrationUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcentrationUnitExists(string id)
        {
          return (_context.ConcentrationUnits?.Any(e => e.ConcentrationCode == id)).GetValueOrDefault();
        }
    }
}
