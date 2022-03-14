using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entidades
{
    public partial class Empacados
    {
        [Key]
        public int EmpacadosId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Concepto { get; set; }

        [ForeignKey("EmpacadosId")]
        public virtual List<ProductosUtilizados> ProductosUtilizados {get;set;} = new List<ProductosUtilizados>();


        [ForeignKey("EmpacadosId")]
        public virtual ProductosProducidos? ProductosProducidos {get;set;} = new ProductosProducidos();

    }
}