using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Views;

//using Domain.Views;
using Persistence.Entities;

namespace Domain.Interfaces
{
    public interface ICliente: IGenericRepository<Cliente>
    {
      
        Task<IEnumerable<ClienteRepresentantes>> GetClienteRepresentantes();
         Task<IEnumerable<Cliente>> GetClientesSinPagos();
          Task<IEnumerable<ClienteRepresentantesApellido>> GetClienteRepresentantesApellido();
          Task<IEnumerable<ClienteSinPagosRepresentantes>> GetClienteSinPagosRepresentantes();
    }
}