using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repository
{
    public class PedidoRepository: IPedido
    {
             private readonly DbAppContext _context;
        public PedidoRepository(DbAppContext context) 
        {
            _context=context;
        }
                  public virtual void Add(Pedido entity)
    {
        _context.Set<Pedido>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Pedido> entities)
    {
        _context.Set<Pedido>().AddRange(entities);
    }

    public virtual IEnumerable<Pedido> Find(Expression<Func<Pedido, bool>> expression)
    {
        return _context.Set<Pedido>().Where(expression);
    }

    public virtual async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Set<Pedido>().ToListAsync();
    }

    public virtual async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Set<Pedido>().FindAsync(id);
    }

    public virtual async Task<Pedido> GetByIdAsync(string id)
    {
        return await _context.Set<Pedido>().FindAsync(id);
    }

        public async Task<IEnumerable<EstadosxPedidos>> GetEstadosxPedidos()
        {
            return await (
                from p in _context.Pedidos
                group p by new {p.Estado} into grp
                select new EstadosxPedidos
                {
                    Estado=grp.Key.Estado,
                    NumeroPedidos=grp.Count()
                }
            ).ToListAsync();
        }

        public virtual void Remove(Pedido entity)
    {
        _context.Set<Pedido>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Pedido> entities)
    {
        _context.Set<Pedido>().RemoveRange(entities);
    }

    public virtual void Update(Pedido entity)
    {
        _context.Set<Pedido>()
            .Update(entity);
    }}
}