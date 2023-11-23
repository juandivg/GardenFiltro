using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Views;
using Persistence.Entities;

namespace API.Profiles
{
    public class MappingProfiles: Profile
    {
         public MappingProfiles()
        {
            CreateMap<Cliente,ClienteDto>().ReverseMap();
         
            CreateMap<Empleado,EmpleadoDto>().ReverseMap();
            CreateMap<GamaProducto,GamaProductoDto>().ReverseMap();
            CreateMap<Oficina,OficinaDto>().ReverseMap();
            CreateMap<Pago,PagoDto>().ReverseMap();
            CreateMap<Pedido,PedidoDto>().ReverseMap();
            CreateMap<Producto,ProductoDto>().ReverseMap();
            CreateMap<FormaPagos,FormaPagosDto>().ReverseMap();
            CreateMap<ClienteRepresentantes,ClienteRepresentantesDto>().ReverseMap();
            CreateMap<EmpleadoJefeDto,Empleado>().ReverseMap();
            CreateMap<EmpleadoJefe2Dto,Empleado>().ReverseMap();
             CreateMap<JefeDto,Empleado>().ReverseMap();
             CreateMap<Producto,ProductoSinPedidosDto>().ReverseMap();
             CreateMap<EstadosxPedidos,EstadosxPedidosDto>().ReverseMap();
              CreateMap<ClienteRepresentantesApellido,ClienteRepresentantesApellidoDto>().ReverseMap();
              CreateMap<ClienteSinPagosRepresentantes,ClienteSinPagosRepresentantesDto>().ReverseMap();


        }
    }
}