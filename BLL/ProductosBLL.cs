using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entidades;
using DAL;

namespace BLL
{
    public partial class ProductosBLL 
    {
        private Contexto _contexto;
        public ProductosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        private bool Existe(int id)
        {
            bool existe = false;

            try{
                existe = _contexto.Productos.Any(p => p.ProductoId == id);
            }catch (Exception e)
            {
                throw e;
            }
            return existe;
        }
        public Productos? ExisteDescripcion(string Descripcion)
        {
            Productos? existe;

            try
            {
                existe = _contexto.Productos
                .Include(x => x.ProductoDetalles)
                .AsNoTracking()
                .Where( p => p.Descripcion.ToLower() == Descripcion.ToLower())
                .AsNoTracking()
                .SingleOrDefault();
            }catch
            {
                throw;
            }
            return existe;
        }
        public bool Guardar (Productos producto)
        {
            if(Existe(producto.ProductoId))
                return Modificar(producto);
            else
                return Insertar(producto);
        }
        private bool Modificar(Productos producto)
        {
            bool paso = false;

            try
            {
                _contexto.Database.ExecuteSqlRaw($"Delete FROM ProductoDetalles where ProductoId={producto.ProductoId}");
                foreach(var anterior in producto.ProductoDetalles)
                {
                    _contexto.Entry(anterior).State= EntityState.Added;
                }
                _contexto.Entry(producto).State = EntityState.Modified;
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            return paso;
        }
        private bool Insertar(Productos producto)
        {
            bool paso = false;
            try
            {
                _contexto.Productos.Add(producto);
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            return paso;
        }
        public Productos? Buscar(int Id)
        {
            Productos? producto;
            try
            {
                producto = _contexto.Productos
                .Include(x => x.ProductoDetalles)
                .Where( p => p.ProductoId == Id)
                .Include( p => p.ProductoDetalles)
                .AsNoTracking()
                .SingleOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return producto;
        }
        public bool Eliminar(int id)
        {
            bool paso = false;

            try
            {
                var producto = _contexto.Productos.Find(id);
                if (producto != null)
                {
                    _contexto.Productos.Remove(producto);
                    paso = _contexto.SaveChanges() > 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return paso;
        }
        public List<Productos> GetList(Expression<Func<Productos, bool>>criterio)
        {
            List<Productos> Lista = new List<Productos>();
            try{
                Lista = _contexto.Productos
                .Include(x => x.ProductoDetalles)
                .Where(criterio)
                .AsNoTracking()
                .ToList();
            }catch(Exception e)
            {
                throw e;
            }
            return Lista;
        }
    }
}