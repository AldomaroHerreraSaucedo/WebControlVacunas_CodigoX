using Microsoft.AspNetCore.Mvc;

namespace WebControlVacunas_CodigoX.Controllers
{
    public class AccesosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
