using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers
{
    public class PedidoController:BaseApiController
    {
            private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>>Get()
        {
            var Pedidos = await _unitOfWork.Pedidos.GetAllAsync();
            return _mapper.Map<List<PedidoDto>>(Pedidos);
        }
    //     }
    //     [HttpGet]
    // [MapToApiVersion("1.1")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<PedidoDto>>> Getpag([FromQuery] Params PedidoParams)
    // {
    //     var Pedido = await _unitOfWork.Pedidos.GetAllAsync(PedidoParams.PageIndex,PedidoParams.PageSize,PedidoParams.Search);
    //     var lstPedidoesDto = _mapper.Map<List<PedidoDto>>(Pedido.registros);
    //     return new Pager<PedidoDto>(lstPedidoesDto,Pedido.totalRegistros,PedidoParams.PageIndex,PedidoParams.PageSize,PedidoParams.Search);
    // }

            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Get2(string id)
    {
        var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
        return _mapper.Map<PedidoDto>(Pedido);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoDto>>Post(PedidoDto PedidoDto)
        {
            var Pedido = _mapper.Map<Pedido>(PedidoDto);

            // if (PedidoDto.Fecha == DateTime.MinValue)
            // {
            //     PedidoDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Pedidos.Add(Pedido);
            await _unitOfWork.SaveAsync();
            
            if(Pedido == null)
            {
                return BadRequest();
            }
            PedidoDto.CodigoPedido = Pedido.CodigoPedido;
            return CreatedAtAction(nameof(Post), new {id = PedidoDto.CodigoPedido}, PedidoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto PedidoDto)
        {
            if(PedidoDto == null)
            {
                return NotFound();
            }
            var Pedidos = _mapper.Map<Pedido>(PedidoDto);
            _unitOfWork.Pedidos.Update(Pedidos);
            await _unitOfWork.SaveAsync();
            return PedidoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
            if(Pedido == null)
            {
                return NotFound();
            }
            _unitOfWork.Pedidos.Remove(Pedido);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
            [HttpGet("GetEstadosxPedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EstadosxPedidosDto>>>Get3()
        {
            var Pedidos = await _unitOfWork.Pedidos.GetEstadosxPedidos();
            return _mapper.Map<List<EstadosxPedidosDto>>(Pedidos);
        }
    }
}