using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repository
{
    public class ClienteRepository :  ICliente
    {
        private readonly DbAppContext _context;
        public ClienteRepository(DbAppContext context) 
        {
            _context=context;
        }
           public virtual void Add(Cliente entity)
    {
        _context.Set<Cliente>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<Cliente> entities)
    {
        _context.Set<Cliente>().AddRange(entities);
    }

    public virtual IEnumerable<Cliente> Find(Expression<Func<Cliente, bool>> expression)
    {
        return _context.Set<Cliente>().Where(expression);
    }

    public virtual async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Set<Cliente>().ToListAsync();
    }

        public Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Set<Cliente>().FindAsync(id);
    }

    public virtual async Task<Cliente> GetByIdAsync(string id)
    {
        return await _context.Set<Cliente>().FindAsync(id);
    }

        public async Task<IEnumerable<ClienteRepresentantes>> GetClienteRepresentantes()
        {
            return await(
                from c in  _context.Clientes.Where(c=>c.Pagos.Count()>0)
                join rep in _context.Empleados on c.CodigoEmpleadoRepVentas equals rep.CodigoEmpleado
                join of in _context.Oficinas on rep.CodigoOficina equals of.CodigoOficina
                select new ClienteRepresentantes
                {
                    NombreCliente=c.NombreCliente,
                    NombreRepresentante=rep.Nombre,
                    CiudadOficina=of.Ciudad
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<ClienteRepresentantesApellido>> GetClienteRepresentantesApellido()
        {  return await(
                from c in  _context.Clientes
                join rep in _context.Empleados on c.CodigoEmpleadoRepVentas equals rep.CodigoEmpleado
                join of in _context.Oficinas on rep.CodigoOficina equals of.CodigoOficina
                select new ClienteRepresentantesApellido
                {
                    NombreCliente=c.NombreCliente,
                    NombreRepresentante=rep.Nombre,
                    ApellidoRepresentante=rep.Apellido1,
                    CiudadOficina=of.Ciudad
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<ClienteSinPagosRepresentantes>> GetClienteSinPagosRepresentantes()
        {
            return await(
                from c in  _context.Clientes.Where(c=>c.Pagos.Count()==0)
                join rep in _context.Empleados on c.CodigoEmpleadoRepVentas equals rep.CodigoEmpleado
                join of in _context.Oficinas on rep.CodigoOficina equals of.CodigoOficina
                select new ClienteSinPagosRepresentantes
                {
                    NombreCliente=c.NombreCliente,
                    NombreRepresentante=rep.Nombre,
                    ApellidoRepresentante=rep.Apellido1,
                    TelefonoOficina=of.Telefono
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientesSinPagos()
        {
            return await _context.Clientes.Where(c=>c.Pagos.Count()==0).ToListAsync();
        }

        public virtual void Remove(Cliente entity)
    {
        _context.Set<Cliente>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<Cliente> entities)
    {
        _context.Set<Cliente>().RemoveRange(entities);
    }

    public virtual void Update(Cliente entity)
    {
        _context.Set<Cliente>()
            .Update(entity);
    }
    }
}