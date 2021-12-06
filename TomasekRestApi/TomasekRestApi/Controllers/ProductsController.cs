using System;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using TomasekRestApi.BL.Data;
using TomasekRestApi.Model.Dto;

namespace TomasekRestApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        /// <summary>
        /// GetAllProducts in system
        /// </summary>
        /// <returns>Returns all products in DB</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var productItems = _repository.GetProducts();
            if (productItems == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
        }

        /// <summary>
        /// GetAllProducts using page and size of page
        /// </summary>
        /// <param name="page">Requested page number</param>
        /// <param name="pageSize">Size of page items</param>
        /// <returns>Return list of products with size</returns>
        [MapToApiVersion("2.0")]
        [HttpGet("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts(int page,int pageSize = 10)
        {
            var productItems = _repository.GetProducts(page,pageSize);
            if (productItems == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
        }

        /// <summary>
        /// Get Product by ID
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>return one product of requested id if exists</returns>
        [HttpGet("{id}", Name = "GetProductsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            var productItem = _repository.GetProductById(id);
            if (productItem == null)
            {
                return NotFound();
            }            
            return Ok(_mapper.Map<ProductReadDto>(productItem));
        }

        //PATCH api/products/{id}
        /// <summary>
        /// Update specification of product with id
        /// </summary>
        /// <param name="id">id of product</param>
        /// <param name="desc">new description for product</param>
        /// <returns>returns no content</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult PartialProductUpdate(int id, string desc)
        {
            var productFromRepo = _repository.GetProductById(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }
            _repository.UpdateProductDescr(id, desc);
            // There should be validation

            return NoContent();
        }



    }
}
