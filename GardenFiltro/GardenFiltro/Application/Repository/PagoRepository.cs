using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repository
{
    public class PagoRepository:  IPago
    {
             private readonly DbAppContext _context;
        public PagoRepository(DbAppContext context) 
        {
            _context=context;
        }
                  public virtual void Add(Pago entity)
    {
        _context.Set<Pago>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Pago> entities)
    {
        _context.Set<Pago>().AddRange(entities);
    }

    public virtual IEnumerable<Pago> Find(Expression<Func<Pago, bool>> expression)
    {
        return _context.Set<Pago>().Where(expression);
    }

    public virtual async Task<IEnumerable<Pago>> GetAllAsync()
    {
        return await _context.Set<Pago>().ToListAsync();
    }

    public virtual async Task<Pago> GetByIdAsync(int id)
    {
        return await _context.Set<Pago>().FindAsync(id);
    }

    public virtual async Task<Pago> GetByIdAsync(string id)
    {
        return await _context.Set<Pago>().FindAsync(id);
    }

        public async Task<IEnumerable<FormaPagos>> GetFormaPagos()
        {
            return await(
                from p in _context.Pagos
                group p by new {p.FormaPago} into grp
                select new FormaPagos
                {
                    FormaPago=grp.Key.FormaPago
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<Pago>> GetPagos2008Paypal()
        {
           return await _context.Pagos.Where(p=>p.FechaPago.Year==2008 && p.FormaPago=="PayPal").OrderByDescending(p=>p.Total).ToListAsync();
        }

        public virtual void Remove(Pago entity)
    {
        _context.Set<Pago>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Pago> entities)
    {
        _context.Set<Pago>().RemoveRange(entities);
    }

    public virtual void Update(Pago entity)
    {
        _context.Set<Pago>()
            .Update(entity);
    }
    }
}