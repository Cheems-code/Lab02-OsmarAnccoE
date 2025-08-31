using System.ComponentModel.DataAnnotations;

namespace Lab02_OsmarAnccoE.Dtos.Empresa;

public class CreateEmpresaDto
{
    [Required]
    [StringLength(200)]
    public string RazonSocial { get; set; }
        
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener 11 caracteres.")]
    public string RUC { get; set; }
        
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }
        
    [StringLength(250)]
    public string Direccion { get; set; }
}