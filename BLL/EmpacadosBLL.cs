using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entidades;

namespace DAL
{
    public partial class EmpacadosBLL
    {
        private Contexto _contexto;
        public EmpacadosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        private bool Existe(int Id)
        {
            bool existe = false;

            try
            {
                existe = _contexto.Empacados.Any(e => e.EmpacadosId == Id);
            }
            catch (Exception e)
            {
                throw e;
            }
            return existe;
        }
        public Empacados? ExisteConcepto(string concepto)
        {
            Empacados? empacado;

            try
            {
                empacado = _contexto.Empacados
                .Include( e => e.ProductosUtilizados)
                .Where( e => e.Concepto.ToLower() == concepto.ToLower())
                .Include( x => x.ProductosUtilizados)
                .ThenInclude( x => x.producto)
                .ThenInclude( x => x.ProductoDetalles)
                .AsNoTracking()
                .SingleOrDefault();
            }catch
            {
                throw;
            }
            return empacado;
        }
        public bool Guardar(Empacados empacado)
        {
            if (Existe(empacado.EmpacadosId))
                return Modificar(empacado);
            else
                return Insertar(empacado);
        }
        private bool Insertar(Empacados empacado)
        {
            bool paso = false;
            try
            {
                foreach (var utilizado in empacado.ProductosUtilizados) //Por cada producto utilizado en mi empaque
                {
                    _contexto.Entry(utilizado).State = EntityState.Added; //Se dice que se añade el utilizado al empaque
                    _contexto.Entry(utilizado.producto).State = EntityState.Modified;  // Se dice que el producto es modificado
                    //Reducimos la existencia
                    utilizado.producto.Existencia -= utilizado.Cantidad;
                    //Recalculamos el valorInventario
                    utilizado.producto.ValorInventario = utilizado.producto.Costo * utilizado.producto.Existencia; 
                }
                var producido = _contexto.Productos.Find(empacado.ProductoId);
                
                if(producido != null)
                {
                    producido.Existencia += empacado.Cantidad;
                    producido.ValorInventario = producido.Costo * producido.Existencia;
                }
                _contexto.Empacados.Add(empacado);
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        private bool Modificar(Empacados empacado) 
        {
            bool paso = false;

            try
            {
                var empacadoAnterior = _contexto.Empacados
                .Where(x => x.EmpacadosId == empacado.EmpacadosId)
                .Include(x => x.ProductosUtilizados)
                .ThenInclude(x => x.producto)
                .ThenInclude(x => x.ProductoDetalles)
                .AsNoTracking()
                .SingleOrDefault();
                if(empacadoAnterior!=null)
                {
                    foreach (var utilizado in empacadoAnterior.ProductosUtilizados)
                    {
                        utilizado.producto.Existencia += utilizado.Cantidad;
                        utilizado.producto.ValorInventario = utilizado.producto.Costo;
                    }
                    
                    var producido = _contexto.Productos.Find(empacado.ProductoId);

                    if(producido!=null)
                    {
                        producido.Existencia -= empacado.Cantidad;
                        producido.ValorInventario = producido.Costo* producido.Existencia;
                    }
                    _contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosUtilizados where EmpacadosId={empacado.EmpacadosId}");



                    foreach (var item in empacado.ProductosUtilizados)
                    {
                        _contexto.Entry(item).State = EntityState.Added;
                        _contexto.Entry(item.producto).State = EntityState.Modified;

                        item.producto.Existencia -= item.Cantidad;
                        item.producto.ValorInventario = item.producto.Costo*item.producto.Existencia;
                    }

                    var producido2 = _contexto.Productos.Find(empacado.ProductoId);

                    if(producido2!=null)
                    {
                        producido2.Existencia += empacado.Cantidad; 
                        producido2.ValorInventario = producido2.Costo*producido2.Existencia;
                    }

                    _contexto.Entry(empacado).State = EntityState.Modified;
                    paso = _contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public Empacados? Buscar(int id)
        {
            Empacados? empacado;

            try
            {
                empacado = _contexto.Empacados
                .Include( e => e.ProductosUtilizados)
                .Where( e => e.EmpacadosId == id)
                .Include( x => x.ProductosUtilizados)
                .ThenInclude( x => x.producto)
                .ThenInclude( x => x.ProductoDetalles)
                .AsNoTracking()
                .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return empacado;
        }
        public bool Eliminar(int id)
        {
            bool paso = false;

            try
            {
                var empacado = _contexto.Empacados.Find(id);
                if (empacado != null)
                {
                    var producido = _contexto.Productos.Find(empacado.ProductoId);
                    if(producido != null)
                    {
                        producido.Existencia -= empacado.Cantidad;
                        producido.ValorInventario = producido.Existencia * producido.Costo;
                    }
                    foreach (var utilizado in empacado.ProductosUtilizados)
                    {
                        _contexto.Entry(utilizado.Empacado).State = EntityState.Modified;
                        _contexto.Entry(utilizado.producto).State = EntityState.Modified;

                        utilizado.producto.Existencia += utilizado.Cantidad;
                        utilizado.producto.ValorInventario  = utilizado.producto.Existencia * utilizado.producto.Costo;
                    }

                    _contexto.Empacados.Remove(empacado);
                    paso = _contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public List<Empacados> GetList(Expression<Func<Empacados, bool>> criterio)
        {
            List<Empacados> Lista = new List<Empacados>();
            try
            {
                Lista = _contexto.Empacados
                .Include(x => x.ProductosUtilizados)
                .ThenInclude(x => x.producto)
                .ThenInclude(x => x.ProductoDetalles)
                .Where(criterio)
                .AsNoTracking()
                .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return Lista;
        }
    }
}