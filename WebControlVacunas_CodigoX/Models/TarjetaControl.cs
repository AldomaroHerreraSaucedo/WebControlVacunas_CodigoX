using System.ComponentModel.DataAnnotations;

namespace WebControlVacunas_CodigoX.Models;

public class TarjetaControl
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo fecha de apertura es obligatorio")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de apertura")]
    public DateTime FechaApertura { get; set; }

    public int NinoId { get; set; }
    public virtual Nino Nino { get; set; }
    public virtual ICollection<CronogramaVacunacion> CronogramasVacunacion { get; set; }
}
