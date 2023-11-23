using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Views
{
    public class ClienteSinPagosRepresentantesDto
    {
        public string NombreCliente { get; set; }
        public string NombreRepresentante { get; set; }
        public string ApellidoRepresentante { get; set; }
        public string TelefonoOficina { get; set; }
    }
}