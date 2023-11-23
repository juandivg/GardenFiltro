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
    
    public class EmpleadoController: BaseApiController
    {
           private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>>Get()
        {
            var Empleados = await _unitOfWork.Empleados.GetAllAsync();
            return _mapper.Map<List<EmpleadoDto>>(Empleados);
        }
    //     [HttpGet]
    // [MapToApiVersion("1.1")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<EmpleadoDto>>> Getpag([FromQuery] Params EmpleadoParams)
    // {
    //     var Empleado = await _unitOfWork.Empleados.GetAllAsync(EmpleadoParams.PageIndex,EmpleadoParams.PageSize,EmpleadoParams.Search);
    //     var lstEmpleadoesDto = _mapper.Map<List<EmpleadoDto>>(Empleado.registros);
    //     return new Pager<EmpleadoDto>(lstEmpleadoesDto,Empleado.totalRegistros,EmpleadoParams.PageIndex,EmpleadoParams.PageSize,EmpleadoParams.Search);
    // }
            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Get2(string id)
    {
        var Empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        return _mapper.Map<EmpleadoDto>(Empleado);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>>Post(EmpleadoDto EmpleadoDto)
        {
            var Empleado = _mapper.Map<Empleado>(EmpleadoDto);

            // if (EmpleadoDto.Fecha == DateTime.MinValue)
            // {
            //     EmpleadoDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Empleados.Add(Empleado);
            await _unitOfWork.SaveAsync();
            
            if(Empleado == null)
            {
                return BadRequest();
            }
            EmpleadoDto.CodigoEmpleado = Empleado.CodigoEmpleado;
            return CreatedAtAction(nameof(Post), new {id = EmpleadoDto.CodigoEmpleado}, EmpleadoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto EmpleadoDto)
        {
            if(EmpleadoDto == null)
            {
                return NotFound();
            }
            var Empleados = _mapper.Map<Empleado>(EmpleadoDto);
            _unitOfWork.Empleados.Update(Empleados);
            await _unitOfWork.SaveAsync();
            return EmpleadoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if(Empleado == null)
            {
                return NotFound();
            }
            _unitOfWork.Empleados.Remove(Empleado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
         [HttpGet("GetJefesdeJefe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoJefeDto>>>Get3()
        {
            var Empleados = await _unitOfWork.Empleados.GetAllAsync();
            return _mapper.Map<List<EmpleadoJefeDto>>(Empleados);
        }
    }
    
}