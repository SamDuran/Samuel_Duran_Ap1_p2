using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entidades;
using DAL;

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
        public bool Guardar(Empacados empacado)
        {
            if (Existe(empacado.EmpacadosId))
                return Modificar(empacado);
            else
                return Insertar(empacado);
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
                .AsNoTracking()
                .SingleOrDefault();

                foreach (var detalle in empacadoAnterior.ProductosUtilizados)
                {
                    detalle.producto.Existencia += detalle.Cantidad;
                }

                _contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosUtilizados where EmpacadosId={empacado.EmpacadosId}");

                foreach (var item in empacado.ProductosUtilizados)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                    _contexto.Entry(item.producto).State = EntityState.Modified;

                    item.producto.Existencia += item.Cantidad;
                }
                _contexto.Entry(empacado).State = EntityState.Modified;
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        private bool Insertar(Empacados empacado)
        {
            bool paso = false;
            try
            {
                _contexto.Empacados.Add(empacado);
                foreach (var utilizado in empacado.ProductosUtilizados)
                {
                    _contexto.Entry(utilizado).State = EntityState.Added;
                    _contexto.Entry(utilizado.producto).State = EntityState.Modified;

                    utilizado.producto.Existencia -= utilizado.Cantidad;
                }
                var producido = _contexto.Productos.Find(empacado.EmpacadosId).Existencia += empacado.ProductosProducidos.Cantidad;
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public Empacados Buscar(int id)
        {
            Empacados? empacado;

            try
            {
                empacado = _contexto.Empacados
                .Include(x => x.ProductosUtilizados)
                .Include(x => x.ProductosProducidos)
                .Where(e => e.EmpacadosId == id).SingleOrDefault();
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
                    _contexto.Empacados.Remove(empacado);
                    foreach (var utilizado in empacado.ProductosUtilizados)
                    {
                        _contexto.Entry(utilizado).State = EntityState.Deleted;
                        _contexto.Entry(utilizado.producto).State = EntityState.Modified;

                        utilizado.producto.Existencia += utilizado.Cantidad;
                    }
                    var producido = _contexto.Productos.Find(empacado.EmpacadosId).Existencia -= empacado.ProductosProducidos.Cantidad;
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
                Lista = _contexto.Empacados.Include(x => x.ProductosProducidos)
                .Include(x => x.ProductosUtilizados).Where(criterio).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return Lista;
        }
    }
}