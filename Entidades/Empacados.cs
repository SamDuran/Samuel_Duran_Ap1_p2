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
        public int Cantidad {get;set;}
        public decimal Peso {get;set;}

        [ForeignKey("EmpacadosId")]
        public virtual List<ProductosUtilizados> ProductosUtilizados {get;set;} = new List<ProductosUtilizados>();
        public int ProductoId { get; set; }

        public Empacados()
        {
            Concepto=String.Empty;
            FechaIngreso = DateTime.Today;
        }
    }
}