using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;

using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers
{
        public class OficinaController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
          [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OficinaDto>>>Get()
        {
            var Oficinas = await _unitOfWork.Oficinas.GetAllAsync();
            return _mapper.Map<List<OficinaDto>>(Oficinas);
        }
    //      [HttpGet]
    // [MapToApiVersion("1.1")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<OficinaDto>>> Getpag([FromQuery] Params OficinaParams)
    // {
    //     var Oficina = await _unitOfWork.Oficinas.GetAllAsync(OficinaParams.PageIndex,OficinaParams.PageSize,OficinaParams.Search);
    //     var lstOficinaesDto = _mapper.Map<List<OficinaDto>>(Oficina.registros);
    //     return new Pager<OficinaDto>(lstOficinaesDto,Oficina.totalRegistros,OficinaParams.PageIndex,OficinaParams.PageSize,OficinaParams.Search);
    // }
            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Get2(string id)
    {
        var Oficina = await _unitOfWork.Oficinas.GetByIdAsync(id);
        return _mapper.Map<OficinaDto>(Oficina);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OficinaDto>>Post(OficinaDto OficinaDto)
        {
            var Oficina = _mapper.Map<Oficina>(OficinaDto);

            // if (OficinaDto.Fecha == DateTime.MinValue)
            // {
            //     OficinaDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Oficinas.Add(Oficina);
            await _unitOfWork.SaveAsync();
            
            if(Oficina == null)
            {
                return BadRequest();
            }
            OficinaDto.CodigoOficina = Oficina.CodigoOficina;
            return CreatedAtAction(nameof(Post), new {id = OficinaDto.CodigoOficina}, OficinaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto OficinaDto)
        {
            if(OficinaDto == null)
            {
                return NotFound();
            }
            var Oficinas = _mapper.Map<Oficina>(OficinaDto);
            _unitOfWork.Oficinas.Update(Oficinas);
            await _unitOfWork.SaveAsync();
            return OficinaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Oficina = await _unitOfWork.Oficinas.GetByIdAsync(id);
            if(Oficina == null)
            {
                return NotFound();
            }
            _unitOfWork.Oficinas.Remove(Oficina);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}