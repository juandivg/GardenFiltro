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
   
    public class PagoController: BaseApiController
    {
         private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PagoDto>>>Get()
        {
            var Pagos = await _unitOfWork.Pagos.GetAllAsync();
            return _mapper.Map<List<PagoDto>>(Pagos);
        }
    //     [HttpGet]
    // [MapToApiVersion("1.1")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<PagoDto>>> Getpag([FromQuery] Params PagoParams)
    // {
    //     var Pago = await _unitOfWork.Pagos.GetAllAsync(PagoParams.PageIndex,PagoParams.PageSize,PagoParams.Search);
    //     var lstPagoesDto = _mapper.Map<List<PagoDto>>(Pago.registros);
    //     return new Pager<PagoDto>(lstPagoesDto,Pago.totalRegistros,PagoParams.PageIndex,PagoParams.PageSize,PagoParams.Search);
    // }

            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Get2(string id)
    {
        var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
        return _mapper.Map<PagoDto>(Pago);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoDto>>Post(PagoDto PagoDto)
        {
            var Pago = _mapper.Map<Pago>(PagoDto);

            // if (PagoDto.Fecha == DateTime.MinValue)
            // {
            //     PagoDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Pagos.Add(Pago);
            await _unitOfWork.SaveAsync();
            
            if(Pago == null)
            {
                return BadRequest();
            }
            PagoDto.IdTransaccion = Pago.IdTransaccion;
            return CreatedAtAction(nameof(Post), new {id = PagoDto.IdTransaccion}, PagoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto PagoDto)
        {
            if(PagoDto == null)
            {
                return NotFound();
            }
            var Pagos = _mapper.Map<Pago>(PagoDto);
            _unitOfWork.Pagos.Update(Pagos);
            await _unitOfWork.SaveAsync();
            return PagoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if(Pago == null)
            {
                return NotFound();
            }
            _unitOfWork.Pagos.Remove(Pago);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
            [HttpGet("GetPagos2008PayPal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PagoDto>>>Get3()
        {
            var Pagos = await _unitOfWork.Pagos.GetPagos2008Paypal();
            return _mapper.Map<List<PagoDto>>(Pagos);
        }
        [HttpGet("GetFormaPagos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FormaPagosDto>>>Get4()
        {
            var Pagos = await _unitOfWork.Pagos.GetFormaPagos();
            return _mapper.Map<List<FormaPagosDto>>(Pagos);
        }
    }
}