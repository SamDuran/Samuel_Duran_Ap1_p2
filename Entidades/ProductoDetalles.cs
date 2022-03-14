using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public partial class ProductoDetalles
    {
        [Key]
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }

        public ProductoDetalles(string descripcion, decimal cantidad, decimal precio)
        {
            Descripcion = descripcion;
            Cantidad = cantidad; 
            Precio = precio;
        }
        public ProductoDetalles(){}
    }
}