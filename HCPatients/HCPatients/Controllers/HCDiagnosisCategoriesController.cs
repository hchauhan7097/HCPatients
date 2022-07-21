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
    public class HCDiagnosisCategoriesController : Controller
    {
        private readonly HCPatientsContext _context;

        public HCDiagnosisCategoriesController(HCPatientsContext context)
        {
            _context = context;
        }

        // GET: HCDiagnosisCategories
        // This method redirect to DiagnosisCategories
        //Index is an default action for an any controller
        //Whenever a controller is called without any action index action will be triggered
        public async Task<IActionResult> Index()
        {
              return _context.DiagnosisCategories != null ? 
                          View(await _context.DiagnosisCategories.ToListAsync()) :
                          Problem("Entity set 'HCPatientsContext.DiagnosisCategories'  is null.");
        }

        // GET: HCDiagnosisCategories/Details/5
        // This method redirect to the Details Page of DiagnosisCategories
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // GET: HCDiagnosisCategories/Create
        // This method redirected to DiagnosisCategories Create page
        public IActionResult Create()
        {
            return View();
        }

        // POST: HCDiagnosisCategories/Create
        // This method handles all the actions on the Create Page of DiagnosisCategories.On this page user can create the records and it will be reflected on database
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnosisCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnosisCategory);
        }

        // GET: HCDiagnosisCategories/Edit/5
        // This method redirects to the Edit page of DiagnosisCategories. On this page, user can edit any record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }
            return View(diagnosisCategory);
        }

        // POST: HCDiagnosisCategories/Edit/5
        // This page will handle all the actions of Edit page i.e. any change on this page will be handled by this function
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (id != diagnosisCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnosisCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisCategoryExists(diagnosisCategory.Id))
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
            return View(diagnosisCategory);
        }

        // GET: HCDiagnosisCategories/Delete/5
        // This method redirects to the Delete page of DiagnosisCategories. On this page, user can delete any record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // POST: HCDiagnosisCategories/Delete/5
        // This method handle all the actions on Delete Page of DiagnosisCategories and it is also reflectd on the page if any action done
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiagnosisCategories == null)
            {
                return Problem("Entity set 'HCPatientsContext.DiagnosisCategories'  is null.");
            }
            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory != null)
            {
                _context.DiagnosisCategories.Remove(diagnosisCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosisCategoryExists(int id)
        {
          return (_context.DiagnosisCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
