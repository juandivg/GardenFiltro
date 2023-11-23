using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repository
{
    public class ProductoRepository:  IProducto
    {
             private readonly DbAppContext _context;
        public ProductoRepository(DbAppContext context)
        {
            _context=context;
        }
                  public virtual void Add(Producto entity)
    {
        _context.Set<Producto>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Producto> entities)
    {
        _context.Set<Producto>().AddRange(entities);
    }

    public virtual IEnumerable<Producto> Find(Expression<Func<Producto, bool>> expression)
    {
        return _context.Set<Producto>().Where(expression);
    }

    public virtual async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Set<Producto>().ToListAsync();
    }

    public virtual async Task<Producto> GetByIdAsync(int id)
    {
        return await _context.Set<Producto>().FindAsync(id);
    }

    public virtual async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Set<Producto>().FindAsync(id);
    }

        public async Task<IEnumerable<Producto>> GetProductosSinPedidos()
        {
            return await _context.Productos.Where(p=>p.DetallePedidos.Count()==0).ToListAsync();
        }

        public virtual void Remove(Producto entity)
    {
        _context.Set<Producto>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Producto> entities)
    {
        _context.Set<Producto>().RemoveRange(entities);
    }

    public virtual void Update(Producto entity)
    {
        _context.Set<Producto>()
            .Update(entity);
    }
    }
}