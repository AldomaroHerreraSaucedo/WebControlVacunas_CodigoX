using System.ComponentModel.DataAnnotations;

namespace WebControlVacunas_CodigoX.Models;

public class CronogramaVacunacion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo dosis es obligatorio")]
    [Display(Name = "Dosis")]
    public string Dosis { get; set; }

    [Required(ErrorMessage = "Campo fecha programada de vacunación es obligatorio")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha programada de vacunación")]
    public DateTime FechaProgramadaVacunacion { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de vacunación")]
    public DateTime? FechaVacunacion { get; set; }

    public int VacunaId { get; set; }
    public virtual Vacuna Vacuna { get; set; }
    public int TarjetaControlId { get; set; }
    public virtual TarjetaControl TarjetaControl { get; set; }
}
