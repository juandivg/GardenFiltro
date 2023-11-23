using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmpleadoJefe2Dto
    {
           public string Nombre { get; set; }
           public JefeDto CodigoJefeNavigation { get; set; }
    }
}