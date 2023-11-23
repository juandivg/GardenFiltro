using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmpleadoJefeDto
    {
           public string Nombre { get; set; }
           public EmpleadoJefe2Dto CodigoJefeNavigation { get; set; }
    }
}