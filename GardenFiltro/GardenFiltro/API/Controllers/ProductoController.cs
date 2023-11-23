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

    public class ProductoController: BaseApiController
    {
         private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>>Get()
        {
            var Productos = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<ProductoDto>>(Productos);
        }
    
            [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Get2(string id)
    {
        var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
        return _mapper.Map<ProductoDto>(Producto);
    }
               [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>>Post(ProductoDto ProductoDto)
        {
            var Producto = _mapper.Map<Producto>(ProductoDto);

            // if (ProductoDto.Fecha == DateTime.MinValue)
            // {
            //     ProductoDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Productos.Add(Producto);
            await _unitOfWork.SaveAsync();
            
            if(Producto == null)
            {
                return BadRequest();
            }
            ProductoDto.CodigoProducto = Producto.CodigoProducto;
            return CreatedAtAction(nameof(Post), new {id = ProductoDto.CodigoProducto}, ProductoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto ProductoDto)
        {
            if(ProductoDto == null)
            {
                return NotFound();
            }
            var Productos = _mapper.Map<Producto>(ProductoDto);
            _unitOfWork.Productos.Update(Productos);
            await _unitOfWork.SaveAsync();
            return ProductoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(string id)
        {
            var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if(Producto == null)
            {
                return NotFound();
            }
            _unitOfWork.Productos.Remove(Producto);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
            [HttpGet("GetProductosSinPedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoSinPedidosDto>>>Get3()
        {
            var Productos = await _unitOfWork.Productos.GetProductosSinPedidos();
            return _mapper.Map<List<ProductoSinPedidosDto>>(Productos);
        }
    }
}