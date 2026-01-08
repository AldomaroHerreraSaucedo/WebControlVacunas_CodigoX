using System.ComponentModel.DataAnnotations;

namespace WebControlVacunas_CodigoX.Models;

public class Vacuna
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo nombre vacuna es obligatorio")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    public virtual ICollection<CronogramaVacunacion> CronogramasVacunacion { get; set; }
}
