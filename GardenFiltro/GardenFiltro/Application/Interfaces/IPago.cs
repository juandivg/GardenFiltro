using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Views;

//using Domain.Views;
using Persistence.Entities;

namespace Domain.Interfaces
{
    public interface IPago: IGenericRepository<Pago>
    {
      
        Task<IEnumerable<Pago>> GetPagos2008Paypal();
        Task<IEnumerable<FormaPagos>> GetFormaPagos();
    }
}