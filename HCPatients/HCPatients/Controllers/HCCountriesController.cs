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
    public class HCCountriesController : Controller
    {
        private readonly HCPatientsContext _context;

        public HCCountriesController(HCPatientsContext context)
        {
            _context = context;
        }

        // GET: HCCountries
        // This method redirects to Countries
        public async Task<IActionResult> Index()
        {
              return _context.Countries != null ? 
                          View(await _context.Countries.ToListAsync()) :
                          Problem("Entity set 'HCPatientsContext.Countries'  is null.");
        }

        // GET: HCCountries/Details/5
        // This method redirects to the Details Page of Countries
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: HCCountries/Create
        // This method redirects to Countries Create page
        public IActionResult Create()
        {
            return View();
        }

        // POST: HCCountries/Create
        // This method handles all the actions on the Create Page of Countries.On this page user can create the records and it will be reflected on database
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: HCCountries/Edit/5
        // This method redirects to the Edit page of Countries. On this page, user can edit any record
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: HCCountries/Edit/5
        // This page will handle all the actions of Edit page i.e. any change on this page will be handled by this function
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (id != country.CountryCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryCode))
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
            return View(country);
        }

        // GET: HCCountries/Delete/5
        // This method redirects to the Delete page of Countries. On this page, user can delete any record
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: HCCountries/Delete/5
        // This method handle all the actions on Delete Page of Countries and it is also reflectd on the page if any action done
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'HCPatientsContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(string id)
        {
          return (_context.Countries?.Any(e => e.CountryCode == id)).GetValueOrDefault();
        }
    }
}
