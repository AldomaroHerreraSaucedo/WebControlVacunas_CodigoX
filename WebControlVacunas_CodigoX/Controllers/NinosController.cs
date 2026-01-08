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
    public class NinosController : Controller
    {
        private readonly ControlVacunasContext _context;

        public NinosController(ControlVacunasContext context)
        {
            _context = context;
        }

        // GET: Ninos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ninos.ToListAsync());
        }

        // GET: Ninos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nino = await _context.ninos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nino == null)
            {
                return NotFound();
            }

            return View(nino);
        }

        // GET: Ninos/Create
        public IActionResult Create()
        {
            ViewData["Genero"] = new List<SelectListItem>
{
                new SelectListItem { Value = "", Text = "Seleccionar género", Selected = true }
            }
            .Concat(Enum.GetValues(typeof(Genero))
                .Cast<Genero>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                })).ToList();

            return View();
        }

        // POST: Ninos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Apellidos,Nombres,Dni,FechaNacimiento,Genero")] Nino nino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nino);
        }

        // GET: Ninos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["Genero"] = new List<SelectListItem>
{
                new SelectListItem { Value = "", Text = "Seleccionar género", Selected = true }
            }
            .Concat(Enum.GetValues(typeof(Genero))
                .Cast<Genero>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                })).ToList();


            if (id == null)
            {
                return NotFound();
            }

            var nino = await _context.ninos.FindAsync(id);
            if (nino == null)
            {
                return NotFound();
            }
            return View(nino);
        }

        // POST: Ninos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Apellidos,Nombres,Dni,FechaNacimiento,Genero")] Nino nino)
        {
            if (id != nino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinoExists(nino.Id))
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
            return View(nino);
        }

        // GET: Ninos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nino = await _context.ninos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nino == null)
            {
                return NotFound();
            }

            return View(nino);
        }

        // POST: Ninos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nino = await _context.ninos.FindAsync(id);
            if (nino != null)
            {
                _context.ninos.Remove(nino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinoExists(int id)
        {
            return _context.ninos.Any(e => e.Id == id);
        }
    }
}
