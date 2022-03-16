using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class ProductoProducido
    {
        [Key]
        public int Id { get; set; }
        public int EmpacadosId { get; set; } 
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Productos Producto { get; set; } = new Productos();
        
    }
}