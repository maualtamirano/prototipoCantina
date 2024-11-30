using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipoCantina.Models;

namespace prototipoCantina.Controllers
{
    public class CreditoesController : Controller
    {
        private readonly cantinabbddContext _context;

        public CreditoesController(cantinabbddContext context)
        {
            _context = context;
        }

        // GET: Creditoes
        public async Task<IActionResult> Index()
        {
            var cantinabbddContext = _context.Creditos.Include(c => c.IdUsuarioNavigation);
            return View(await cantinabbddContext.ToListAsync());
        }

        // GET: Creditoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Creditos == null)
            {
                return NotFound();
            }

            var credito = await _context.Creditos
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCredito == id);
            if (credito == null)
            {
                return NotFound();
            }

            return View(credito);
        }

        // GET: Creditoes/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Creditoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCredito,IdUsuario,CreditoDiario,CreditoConsumido,Fecha")] Credito credito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(credito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", credito.IdUsuario);
            return View(credito);
        }

        // GET: Creditoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Creditos == null)
            {
                return NotFound();
            }

            var credito = await _context.Creditos.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", credito.IdUsuario);
            return View(credito);
        }

        // POST: Creditoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCredito,IdUsuario,CreditoDiario,CreditoConsumido,Fecha")] Credito credito)
        {
            if (id != credito.IdCredito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditoExists(credito.IdCredito))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", credito.IdUsuario);
            return View(credito);
        }

        // GET: Creditoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Creditos == null)
            {
                return NotFound();
            }

            var credito = await _context.Creditos
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCredito == id);
            if (credito == null)
            {
                return NotFound();
            }

            return View(credito);
        }

        // POST: Creditoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Creditos == null)
            {
                return Problem("Entity set 'cantinabbddContext.Creditos'  is null.");
            }
            var credito = await _context.Creditos.FindAsync(id);
            if (credito != null)
            {
                _context.Creditos.Remove(credito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditoExists(int id)
        {
          return (_context.Creditos?.Any(e => e.IdCredito == id)).GetValueOrDefault();
        }
    }
}

