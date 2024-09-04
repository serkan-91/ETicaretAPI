using EticaretAPI.API.Pages;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ViewModels.Products;
using EticaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController(
        IProductWriteRepository _productWriteRepository ,
        IProductReadRepository _productReadRepository ,
        IOrderWriteRepository _orderWriteRepository ,
        IOrderReadRepository _orderReadRepository ,
        ICustomerWriteRepository _customerWriteRepository
        ) : ControllerBase
    {
        [HttpGet]
        public async Task CreateOrder()
        {
            var customerId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId , Name = "Muhiddin" });
            await _orderWriteRepository.AddAsync(new()
            {
                Description = "bla bla bla" ,
                Address = "Ankara Cankaya" ,
                CustomerId = customerId
            });
            await _orderWriteRepository.AddAsync(new()
            {
                Description = "bla bla bla 2" ,
                Address = "Ankara Pusaklar" ,
                CustomerId = customerId
            });
            await _orderWriteRepository.SaveAsync();
        }

        [HttpGet]
        public async Task UpdateOrder()
        {
            Order order = await _orderReadRepository.GetByIdAsync("3850b0bb-77c5-48f5-9fff-7d39962d0534");
            order.Address = "Istanbl";
            await _orderWriteRepository.SaveAsync();
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(string id)
        {
            Product getProduct =   await _productReadRepository.FindAsync(id);
            return Ok(getProduct);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsWithPage([FromQuery] Paginations pagination)
        {
            var totalCount = await _productReadRepository.GetAll(false).CountAsync();
            var products = await _productReadRepository.GetAll(false)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Stock,
                        p.Price,
                        p.CreatedDate,
                        p.UpdatedDate
                    })
                    .Skip(pagination.Page * pagination.Size)
                    .Take(pagination.Size)
                    .ToListAsync();

            return Ok(new
            {
                products ,
                totalCount
            });
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    //return Ok(await _productReadRepository.GetByIdAsync(id));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name ,
                Price = model.Price ,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }
    }
}
