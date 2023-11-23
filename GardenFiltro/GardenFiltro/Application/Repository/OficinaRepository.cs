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
    public class OficinaRepository: IOficina
    {
             private readonly DbAppContext _context;
        public OficinaRepository(DbAppContext context) 
        {
            _context=context;
        }
                  public virtual void Add(Oficina entity)
    {
        _context.Set<Oficina>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Oficina> entities)
    {
        _context.Set<Oficina>().AddRange(entities);
    }

    public virtual IEnumerable<Oficina> Find(Expression<Func<Oficina, bool>> expression)
    {
        return _context.Set<Oficina>().Where(expression);
    }

    public virtual async Task<IEnumerable<Oficina>> GetAllAsync()
    {
        return await _context.Set<Oficina>().ToListAsync();
    }

    public virtual async Task<Oficina> GetByIdAsync(int id)
    {
        return await _context.Set<Oficina>().FindAsync(id);
    }

    public virtual async Task<Oficina> GetByIdAsync(string id)
    {
        return await _context.Set<Oficina>().FindAsync(id);
    }

    public virtual void Remove(Oficina entity)
    {
        _context.Set<Oficina>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Oficina> entities)
    {
        _context.Set<Oficina>().RemoveRange(entities);
    }

    public virtual void Update(Oficina entity)
    {
        _context.Set<Oficina>()
            .Update(entity);
    }
    }
}