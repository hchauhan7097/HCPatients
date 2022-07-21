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
    public class HCMedicationTypesController : Controller
    {
        private readonly HCPatientsContext _context;

        public HCMedicationTypesController(HCPatientsContext context)
        {
            _context = context;
        }

        // GET: HCMedicationTypes
        // This method redirect to MedicationTypes
        public async Task<IActionResult> Index()
        {
              return _context.MedicationTypes != null ? 
                          View(await _context.MedicationTypes.ToListAsync()) :
                          Problem("Entity set 'HCPatientsContext.MedicationTypes'  is null.");
        }

        // GET: HCMedicationTypes/Details/5
        // This method redirect to the Details Page of MedicationTypes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // GET: HCMedicationTypes/Create
        // This method redirected to MedicationTypes Create page
        public IActionResult Create()
        {
            return View();
        }

        // POST: HCMedicationTypes/Create
        // This method handles all the actions on the Create Page of MedicationTypes.On this page user can create the records and it will be reflected on database
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicationType);
        }

        // GET: HCMedicationTypes/Edit/5
        // This method redirects to the Edit page of MedicationTypes. On this page, user can edit any record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType == null)
            {
                return NotFound();
            }
            return View(medicationType);
        }

        // POST: HCMedicationTypes/Edit/5
        // This page will handle all the actions of Edit page i.e. any change on this page will be handled by this function
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId))
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
            return View(medicationType);
        }

        // GET: HCMedicationTypes/Delete/5
        // This method redirects to the Delete page of MedicationTypes. On this page, user can delete any record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // POST: HCMedicationTypes/Delete/5
        // This method handle all the actions on Delete Page of MedicationTypes and it is also reflectd on the page if any action done
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicationTypes == null)
            {
                return Problem("Entity set 'HCPatientsContext.MedicationTypes'  is null.");
            }
            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType != null)
            {
                _context.MedicationTypes.Remove(medicationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationTypeExists(int id)
        {
          return (_context.MedicationTypes?.Any(e => e.MedicationTypeId == id)).GetValueOrDefault();
        }
    }
}
