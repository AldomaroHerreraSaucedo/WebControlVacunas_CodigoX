using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebControlVacunas_CodigoX.Data;
using WebControlVacunas_CodigoX.Models;

namespace WebControlVacunas_CodigoX.Controllers
{
    public class CronogramasVacunacionController : Controller
    {
        private readonly ControlVacunasContext _context;

        public CronogramasVacunacionController(ControlVacunasContext context)
        {
            _context = context;
        }

        // GET: CronogramasVacunacion
        public async Task<IActionResult> Index()
        {
            var controlVacunasContext = _context.CronogramasVacunacion.Include(c => c.TarjetaControl).Include(c => c.Vacuna);
            return View(await controlVacunasContext.ToListAsync());
        }

        // GET: CronogramasVacunacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cronogramaVacunacion = await _context.CronogramasVacunacion
                .Include(c => c.TarjetaControl)
                .Include(c => c.Vacuna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cronogramaVacunacion == null)
            {
                return NotFound();
            }

            return View(cronogramaVacunacion);
        }

        // GET: CronogramasVacunacion/Create
        public IActionResult Create()
        {
            ViewData["TarjetaControlId"] = new SelectList(_context.TarjetasControl, "Id", "Id");
            ViewData["VacunaId"] = new SelectList(_context.Vacunas, "Id", "Nombre");
            return View();
        }

        // POST: CronogramasVacunacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dosis,FechaProgramadaVacunacion,FechaVacunacion,VacunaId,TarjetaControlId")] CronogramaVacunacion cronogramaVacunacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cronogramaVacunacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TarjetaControlId"] = new SelectList(_context.TarjetasControl, "Id", "Id", cronogramaVacunacion.TarjetaControlId);
            ViewData["VacunaId"] = new SelectList(_context.Vacunas, "Id", "Nombre", cronogramaVacunacion.VacunaId);
            return View(cronogramaVacunacion);
        }

        // GET: CronogramasVacunacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cronogramaVacunacion = await _context.CronogramasVacunacion.FindAsync(id);
            if (cronogramaVacunacion == null)
            {
                return NotFound();
            }
            ViewData["TarjetaControlId"] = new SelectList(_context.TarjetasControl, "Id", "Id", cronogramaVacunacion.TarjetaControlId);
            ViewData["VacunaId"] = new SelectList(_context.Vacunas, "Id", "Nombre", cronogramaVacunacion.VacunaId);
            return View(cronogramaVacunacion);
        }

        // POST: CronogramasVacunacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dosis,FechaProgramadaVacunacion,FechaVacunacion,VacunaId,TarjetaControlId")] CronogramaVacunacion cronogramaVacunacion)
        {
            if (id != cronogramaVacunacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cronogramaVacunacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CronogramaVacunacionExists(cronogramaVacunacion.Id))
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
            ViewData["TarjetaControlId"] = new SelectList(_context.TarjetasControl, "Id", "Id", cronogramaVacunacion.TarjetaControlId);
            ViewData["VacunaId"] = new SelectList(_context.Vacunas, "Id", "Nombre", cronogramaVacunacion.VacunaId);
            return View(cronogramaVacunacion);
        }

        // GET: CronogramasVacunacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cronogramaVacunacion = await _context.CronogramasVacunacion
                .Include(c => c.TarjetaControl)
                .Include(c => c.Vacuna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cronogramaVacunacion == null)
            {
                return NotFound();
            }

            return View(cronogramaVacunacion);
        }

        // POST: CronogramasVacunacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cronogramaVacunacion = await _context.CronogramasVacunacion.FindAsync(id);
            if (cronogramaVacunacion != null)
            {
                _context.CronogramasVacunacion.Remove(cronogramaVacunacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CronogramaVacunacionExists(int id)
        {
            return _context.CronogramasVacunacion.Any(e => e.Id == id);
        }
    }
}
