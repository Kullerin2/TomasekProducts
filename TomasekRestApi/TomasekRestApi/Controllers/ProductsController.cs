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
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        //GET api/products
        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var productItems = _repository.GetProducts();
            if (productItems == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
        }
        
        //GET api/products
        [MapToApiVersion("2.0")]
        [HttpGet("{page}")]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts(int page,int pageSize = 10)
        {
            var productItems = _repository.GetProducts(page,pageSize);
            if (productItems == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
        }

        //GET api/products/{id}
        [HttpGet("{id}", Name = "GetProductsById")]
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
        [HttpPatch("{id}")]
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
