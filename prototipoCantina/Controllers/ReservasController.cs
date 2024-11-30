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
    public class ReservasController : Controller
    {
        private readonly cantinabbddContext _context;

        public ReservasController(cantinabbddContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var cantinabbddContext = _context.Reservas.Include(r => r.IdMenuNavigation).Include(r => r.IdUsuarioNavigation);
            return View(await cantinabbddContext.ToListAsync());
        }



        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdMenuNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "NombreMenu");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            return View();

       

        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,IdUsuario,IdMenu,FechaReserva,MontoTotal")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {

                // Obtén el crédito del usuario asociado a la reserva
                var credito = await _context.Creditos
                    .FirstOrDefaultAsync(c => c.IdUsuario == reserva.IdUsuario);

                // Verifica si el crédito es suficiente
                if (credito == null || credito.CreditoDisponible < reserva.MontoTotal)
                {
                    ModelState.AddModelError("", "Crédito insuficiente para realizar la reserva.");
                    ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "IdMenu", reserva.IdMenu);
                    ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", reserva.IdUsuario);
                    return View(reserva);
                }

                // Actualiza el crédito consumido del usuario
                credito.CreditoConsumido += reserva.MontoTotal;

                // Guarda la reserva y los cambios en el crédito
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "IdMenu", reserva.IdMenu);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", reserva.IdUsuario);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "NombreMenu", reserva.IdMenu);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", reserva.IdUsuario);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdUsuario,IdMenu,FechaReserva,MontoTotal")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
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
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "IdMenu", reserva.IdMenu);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", reserva.IdUsuario);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdMenuNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'cantinabbddContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reservas?.Any(e => e.IdReserva == id)).GetValueOrDefault();
        }
    }
}

