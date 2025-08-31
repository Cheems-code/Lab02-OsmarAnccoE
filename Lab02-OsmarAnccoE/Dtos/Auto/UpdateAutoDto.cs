using System.ComponentModel.DataAnnotations;

namespace Lab02_OsmarAnccoE.Dtos.Auto;

public class UpdateAutoDto
{
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "La placa no puede tener más de 10 caracteres.")]
        public string Placa { get; set; }

}