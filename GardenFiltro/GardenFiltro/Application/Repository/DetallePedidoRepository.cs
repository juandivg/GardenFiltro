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
    public class DetallePedidoRepository: IDetallePedido
    {
             private readonly DbAppContext _context;
        public DetallePedidoRepository(DbAppContext context)
        {
            _context=context;
        }
                  public virtual void Add(DetallePedido entity)
    {
        _context.Set<DetallePedido>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<DetallePedido> entities)
    {
        _context.Set<DetallePedido>().AddRange(entities);
    }

    public virtual IEnumerable<DetallePedido> Find(Expression<Func<DetallePedido, bool>> expression)
    {
        return _context.Set<DetallePedido>().Where(expression);
    }

    public virtual async Task<IEnumerable<DetallePedido>> GetAllAsync()
    {
        return await _context.Set<DetallePedido>().ToListAsync();
    }

        public Task<(int totalRegistros, IEnumerable<DetallePedido> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DetallePedido> GetByIdAsync(int id)
    {
        return await _context.Set<DetallePedido>().FindAsync(id);
    }

    public virtual async Task<DetallePedido> GetByIdAsync(string id)
    {
        return await _context.Set<DetallePedido>().FindAsync(id);
    }

    public virtual void Remove(DetallePedido entity)
    {
        _context.Set<DetallePedido>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<DetallePedido> entities)
    {
        _context.Set<DetallePedido>().RemoveRange(entities);
    }

    public virtual void Update(DetallePedido entity)
    {
        _context.Set<DetallePedido>()
            .Update(entity);
    }
    }
}