using System.ComponentModel.DataAnnotations;

namespace GestionProductos.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]{3,50}$", 
            ErrorMessage = "El nombre solo puede contener letras, números y espacios (entre 3 y 50 caracteres).")]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", 
            ErrorMessage = "El precio debe ser un número válido con hasta dos decimales (ej. 10.50).")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 100000, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}