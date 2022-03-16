using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades{

    public partial class ProductosUtilizados
    {
        [Key]
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public int EmpacadosId {get;set;}
        public string Descripcion {get;set;}

        [ForeignKey("ProductoId")] 
        public Productos producto {get;set;} = new Productos();

    }
}