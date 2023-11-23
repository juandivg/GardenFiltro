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
    public class EmpleadoRepository: IEmpleado
    {
             private readonly DbAppContext _context;
        public EmpleadoRepository(DbAppContext context) 
        {
            _context=context;
        }
                  public virtual void Add(Empleado entity)
    {
        _context.Set<Empleado>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Empleado> entities)
    {
        _context.Set<Empleado>().AddRange(entities);
    }

    public virtual IEnumerable<Empleado> Find(Expression<Func<Empleado, bool>> expression)
    {
        return _context.Set<Empleado>().Where(expression);
    }

    public virtual async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Set<Empleado>().ToListAsync();
    }

        public Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Set<Empleado>().FindAsync(id);
    }

    public virtual async Task<Empleado> GetByIdAsync(string id)
    {
        return await _context.Set<Empleado>().FindAsync(id);
    }

    public virtual void Remove(Empleado entity)
    {
        _context.Set<Empleado>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Empleado> entities)
    {
        _context.Set<Empleado>().RemoveRange(entities);
    }

    public virtual void Update(Empleado entity)
    {
        _context.Set<Empleado>()
            .Update(entity);
    }
    }
}