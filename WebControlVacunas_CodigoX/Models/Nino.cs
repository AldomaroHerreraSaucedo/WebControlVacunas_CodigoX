using System.ComponentModel.DataAnnotations;

namespace WebControlVacunas_CodigoX.Models;

public class Nino
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo apellidos del niño es obligatorio")]
    [Display(Name = "Apellidos")]
    public string Apellidos { get; set; }


    [Required(ErrorMessage = "Campo nombres del niño es obligatorio")]
    [Display(Name = "Nombrs")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Campo DNI es obligatorio")]
    [Display(Name = "DNI")]
    public string Dni { get; set; }

    [Required(ErrorMessage = "Campo fecha de nacimiento es obligatorio")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "Campo género es obligatorio")]
    [Display(Name = "Género")]
    public Genero Genero { get; set; }

    public virtual TarjetaControl TarjetaControl { get; set; }
}

public enum Genero
{
    MASCULINO = 1,
    FEMENINO = 2
}
