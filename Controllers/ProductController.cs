using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CrudAPI.Models;
using CrudAPI.DTOs;
using CrudAPI.Repositories;
using AutoMapper;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper Mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            var productDTOs = Mapper.Map<List<ProductDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = Mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            var product = Mapper.Map<Product>(productDTO);
            _productRepository.AddProduct(product);
            var createdProductDTO = Mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, createdProductDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO productDTO)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            Mapper.Map(productDTO, existingProduct);
            _productRepository.UpdateProduct(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(id);
            return NoContent();
        }

        // Add other actions as needed for additional functionality

    }
}
