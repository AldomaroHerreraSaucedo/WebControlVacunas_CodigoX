using Microsoft.EntityFrameworkCore;
using WebControlVacunas_CodigoX.Models;

namespace WebControlVacunas_CodigoX.Data
{
    public class BDInicializa
    {
        public static void Inicializar(ControlVacunasContext context)
        {
            if (context.Vacunas.Any())
            {
                return;
            }

            var vacuna = new Vacuna[]
            {
                new Vacuna { Nombre = "BCG"},
                new Vacuna { Nombre = "Hepatitis R.N"},
                new Vacuna { Nombre = "Hexavalente"},
                new Vacuna { Nombre = "Rotavirus"},
                new Vacuna { Nombre = "Neumococo"},
                new Vacuna { Nombre = "Incfluenza"},
                new Vacuna { Nombre = "Trivirica"},
                new Vacuna { Nombre = "Hepatitis A"},
                new Vacuna { Nombre = "Vaaricela"},
                new Vacuna { Nombre = "Antiamarilica"},
                new Vacuna { Nombre = "Pentavalente"},
            };
            context.Vacunas.AddRange(vacuna);
            context.SaveChanges();
        }
    }
}
