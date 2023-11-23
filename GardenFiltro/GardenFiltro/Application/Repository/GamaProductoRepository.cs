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
    public class GamaProductoRepository: IGamaProducto
    {
             private readonly DbAppContext _context;
        public GamaProductoRepository(DbAppContext context) 
        {
            _context=context;
        }
                  public virtual void Add(GamaProducto entity)
    {
        _context.Set<GamaProducto>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<GamaProducto> entities)
    {
        _context.Set<GamaProducto>().AddRange(entities);
    }

    public virtual IEnumerable<GamaProducto> Find(Expression<Func<GamaProducto, bool>> expression)
    {
        return _context.Set<GamaProducto>().Where(expression);
    }

    public virtual async Task<IEnumerable<GamaProducto>> GetAllAsync()
    {
        return await _context.Set<GamaProducto>().ToListAsync();
    }

        public Task<(int totalRegistros, IEnumerable<GamaProducto> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GamaProducto> GetByIdAsync(int id)
    {
        return await _context.Set<GamaProducto>().FindAsync(id);
    }

    public virtual async Task<GamaProducto> GetByIdAsync(string id)
    {
        return await _context.Set<GamaProducto>().FindAsync(id);
    }

    public virtual void Remove(GamaProducto entity)
    {
        _context.Set<GamaProducto>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<GamaProducto> entities)
    {
        _context.Set<GamaProducto>().RemoveRange(entities);
    }

    public virtual void Update(GamaProducto entity)
    {
        _context.Set<GamaProducto>()
            .Update(entity);
    }
    }
}