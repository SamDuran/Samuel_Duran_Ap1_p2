using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entidades
{
    public partial class Productos
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe indicar la descripción.")]
        [MinLength(3, ErrorMessage = "La descripción debe tener al menos {1} caractéres.")]
        [MaxLength(35, ErrorMessage = "La descripción no debe pasar de {1} caractéres.")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Campo obligatorio. Se debe indicar la existencia.")]
        [Range(0, 5000000, ErrorMessage = "Se debe indicar la existencia del producto dentro de los rangos {1}/{2}.")]
        public decimal Existencia { get; set; }

        [Required(ErrorMessage = "El Campo \"Costo\"está vacío. Por favor indique un costo.")]
        [Range(1, int.MaxValue, ErrorMessage = "El costo debe estar dentro del rango permitido {1}/{2}.")] 
        public decimal Costo { get; set; }

        public decimal ValorInventario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio. Se debe indicar el precio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Se debe indicar el precio del producto dentro de los rangos {1}/{2}.")]
        public decimal Precio { get; set; }
        
        public decimal Ganancia { get; set; }

        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("ProductoId")]
        public virtual List<ProductoDetalles> ProductoDetalles { get; set; } = new List<ProductoDetalles>();
    }
}