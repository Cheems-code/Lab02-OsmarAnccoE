using System.ComponentModel.DataAnnotations;

namespace Lab02_OsmarAnccoE.Dtos.Persona;

public class CreatePersonaDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no puede tener mas de 100 letras")]
    public string nombre { get; set; }
    
    [Required(ErrorMessage = "El DNI es obligatorio")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI no puede tener mas de 8 caracteres")]
    public string DNI { get; set; }
}