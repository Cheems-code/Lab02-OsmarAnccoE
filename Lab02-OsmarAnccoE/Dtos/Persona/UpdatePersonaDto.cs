using System.ComponentModel.DataAnnotations;

namespace Lab02_OsmarAnccoE.Dtos.Persona;

public class UpdatePersonaDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string nombre { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener exactamente 8 caracteres.")]
    public string DNI { get; set; }
}
