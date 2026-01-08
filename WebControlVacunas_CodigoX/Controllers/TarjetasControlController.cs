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
    public class TarjetasControlController : Controller
    {
        private readonly ControlVacunasContext _context;

        public TarjetasControlController(ControlVacunasContext context)
        {
            _context = context;
        }

        // GET: TarjetasControl
        public async Task<IActionResult> Index()
        {
            var controlVacunasContext = _context.TarjetasControl.Include(t => t.Nino);
            return View(await controlVacunasContext.ToListAsync());
        }

        // GET: TarjetasControl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetaControl = await _context.TarjetasControl
                .Include(t => t.Nino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarjetaControl == null)
            {
                return NotFound();
            }

            return View(tarjetaControl);
        }

        // GET: TarjetasControl/Create
        public IActionResult Create(string keyword)
        {
            Nino ninoEncontrado = null;

            if (!string.IsNullOrEmpty(keyword))
            {
                ninoEncontrado = _context.ninos
                    .FirstOrDefault(n => n.Dni.Contains(keyword));
            }

            ViewBag.FechaApertura = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.NinoEncontrado = ninoEncontrado;

            return View();
        }

        /*
        // POST: TarjetasControl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaApertura,NinoId")] TarjetaControl tarjetaControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjetaControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NinoId"] = new SelectList(_context.ninos, "Id", "Apellidos", tarjetaControl.NinoId);


            var fechaNacimiento = tarjetaControl.Nino.FechaNacimiento;

            


            return View(tarjetaControl);
        }

        */

        // POST: TarjetasControl/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaApertura,NinoId")] TarjetaControl tarjetaControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjetaControl);
                await _context.SaveChangesAsync();

                var nino = await _context.ninos.FindAsync(tarjetaControl.NinoId);
                if (nino != null)
                {
                    var fechaNacimiento = nino.FechaNacimiento;

                    var vacunas = new Dictionary<int, List<int>>() {
                        { 1, new List<int> { 0 } },
                        { 2, new List<int> { 0 } },
                        { 3, new List<int> { 2, 4, 6 } },
                        { 4, new List<int> { 2, 4 } },
                        { 5, new List<int> { 2, 4, 6, 15 } },
                        { 6, new List<int> { 7, 8 } },
                        { 7, new List<int> { 12, 48 } },
                        { 8, new List<int> { 12, 15, 18, 48 } },
                        { 9, new List<int> { 12, 15, 18, 48 } },
                        { 10, new List<int> { 15, 18, 48} },
                        { 11, new List<int> { 18, 48 } }
                     };

                    foreach (var vacuna in vacunas)
                    {
                        int vacunaId = vacuna.Key;
                        var meses = vacuna.Value;

                        for (int i = 0; i < meses.Count; i++)
                        {
                            var cronograma = new CronogramaVacunacion
                            {
                                Dosis = $"Dosis {i + 1}",
                                FechaProgramadaVacunacion = fechaNacimiento.AddMonths(meses[i]),
                                FechaVacunacion = null,
                                VacunaId = vacunaId,
                                TarjetaControlId = tarjetaControl.Id
                            };

                            _context.CronogramasVacunacion.Add(cronograma);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["NinoId"] = new SelectList(_context.ninos, "Id", "Apellidos", tarjetaControl.NinoId);
            return View(tarjetaControl);
        }



        // GET: TarjetasControl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetaControl = await _context.TarjetasControl.FindAsync(id);
            if (tarjetaControl == null)
            {
                return NotFound();
            }
            ViewData["NinoId"] = new SelectList(_context.ninos, "Id", "Apellidos", tarjetaControl.NinoId);
            return View(tarjetaControl);
        }

        // POST: TarjetasControl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaApertura,NinoId")] TarjetaControl tarjetaControl)
        {
            if (id != tarjetaControl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjetaControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaControlExists(tarjetaControl.Id))
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
            ViewData["NinoId"] = new SelectList(_context.ninos, "Id", "Apellidos", tarjetaControl.NinoId);
            return View(tarjetaControl);
        }

        // GET: TarjetasControl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetaControl = await _context.TarjetasControl
                .Include(t => t.Nino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarjetaControl == null)
            {
                return NotFound();
            }

            return View(tarjetaControl);
        }

        // POST: TarjetasControl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarjetaControl = await _context.TarjetasControl.FindAsync(id);
            if (tarjetaControl != null)
            {
                _context.TarjetasControl.Remove(tarjetaControl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaControlExists(int id)
        {
            return _context.TarjetasControl.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarNinoPorDNI(string dni)
        {
            if (string.IsNullOrEmpty(dni))
                return Json(new { success = false, message = "Debe ingresar un DNI." });

            var ninos = await _context.ninos
                .Where(n => n.Dni.Contains(dni))
                .Select(n => new
                {
                    n.Id,
                    n.Nombres,
                    n.Apellidos,
                    n.Dni
                })
                .ToListAsync();

            if (ninos.Count == 0)
                return Json(new { success = false, message = "No se encontraron niños con ese DNI." });

            return Json(new { success = true, data = ninos });
        }

    }
}
