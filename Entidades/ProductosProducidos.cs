using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class ProductosProducidos
    {
        [Key]
        public int Id { get; set; }
        public int EmpacadosId { get; set; }
        public decimal Cantidad { get; set; }
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; } = new Productos();
        
        public string Descripcion { get; set; }
    }
}