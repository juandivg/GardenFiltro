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

    public class ClienteController: BaseApiController
    {
         private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteDto>>>Get()
        {
            var Clientes = await _unitOfWork.Clientes.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(Clientes);
        }
    //     [HttpGet]
    // [MapToApiVersion("1.1")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<ClienteDto>>> Getpag([FromQuery] Params ClienteParams)
    // {
    //     var Cliente = await _unitOfWork.Clientes.GetAllAsync(ClienteParams.PageIndex,ClienteParams.PageSize,ClienteParams.Search);
    //     var lstClienteesDto = _mapper.Map<List<ClienteDto>>(Cliente.registros);
    //     return new Pager<ClienteDto>(lstClienteesDto,Cliente.totalRegistros,ClienteParams.PageIndex,ClienteParams.PageSize,ClienteParams.Search);
    // }
            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Get2(string id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        return _mapper.Map<ClienteDto>(cliente);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>>Post(ClienteDto ClienteDto)
        {
            var Cliente = _mapper.Map<Cliente>(ClienteDto);

            // if (ClienteDto.Fecha == DateTime.MinValue)
            // {
            //     ClienteDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Clientes.Add(Cliente);
            await _unitOfWork.SaveAsync();
            
            if(Cliente == null)
            {
                return BadRequest();
            }
            ClienteDto.CodigoCliente = Cliente.CodigoCliente;
            return CreatedAtAction(nameof(Post), new {id = ClienteDto.CodigoCliente}, ClienteDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto ClienteDto)
        {
            if(ClienteDto == null)
            {
                return NotFound();
            }
            var Clientes = _mapper.Map<Cliente>(ClienteDto);
            _unitOfWork.Clientes.Update(Clientes);
            await _unitOfWork.SaveAsync();
            return ClienteDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if(Cliente == null)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Remove(Cliente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
            [HttpGet ("GetClientesRepresentantes")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteRepresentantesDto>>>Get3()
        {
            var Clientes = await _unitOfWork.Clientes.GetClienteRepresentantes();
            return _mapper.Map<List<ClienteRepresentantesDto>>(Clientes);
        }
        [HttpGet ("GetClientesSinPagos")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteDto>>>Get4()
        {
            var Clientes = await _unitOfWork.Clientes.GetClientesSinPagos();
            return _mapper.Map<List<ClienteDto>>(Clientes);
        }
          [HttpGet ("GetClienteRepresentantesApellido")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteRepresentantesApellidoDto>>>Get5()
        {
            var Clientes = await _unitOfWork.Clientes.GetClienteRepresentantesApellido();
            return _mapper.Map<List<ClienteRepresentantesApellidoDto>>(Clientes);
        }
        [HttpGet ("GetClienteSinPagosRepresentantes")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteSinPagosRepresentantesDto>>>Get6()
        {
            var Clientes = await _unitOfWork.Clientes.GetClienteSinPagosRepresentantes();
            return _mapper.Map<List<ClienteSinPagosRepresentantesDto>>(Clientes);
        }
 
    }
}