﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipoCantina.Models;

namespace prototipoCantina.Controllers
{
    public class MenusController : Controller
    {
        private readonly cantinabbddContext _context;

        public MenusController(cantinabbddContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
              return _context.Menus != null ? 
                          View(await _context.Menus.ToListAsync()) :
                          Problem("Entity set 'cantinabbddContext.Menus'  is null.");
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.IdMenu == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMenu,NombreMenu,Precio,Descripcion")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMenu,NombreMenu,Precio,Descripcion")] Menu menu)
        {
            if (id != menu.IdMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.IdMenu))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.IdMenu == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'cantinabbddContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menus?.Any(e => e.IdMenu == id)).GetValueOrDefault();
        }
    }
}
