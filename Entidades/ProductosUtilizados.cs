using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades{

    public partial class ProductosUtilizados
    {
        [Key]
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public int ProductosId { get; set; }

        [ForeignKey("ProductoId")] 
        public virtual Productos producto {get;set;} = new Productos();

        public string Descripcion {get;set;}
    }
}