using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models;
using StockControlAPI.Models.Entities;
using StockControlAPI.Models.ViewModels;

namespace StockControlAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Basic")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public StockApiDBContext _stockApiDBContext;
        public StockController(StockApiDBContext stockApiDBContext)
        {
            _stockApiDBContext = stockApiDBContext;
        }

        [HttpGet]
        //public List<ViewProduct> GetAllProducts()
        public async Task<IActionResult> GetAllProducts()
        {
            List<ViewProduct> products = await _stockApiDBContext.Products
                .Include(p => p.Category)
                .Include(p => p.Warehouse)
                .Select(p => new ViewProduct
                {   
                    //Id = p.Id, 
                    ProductName = p.ProductName,
                    Count = p.Count,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    WarehouseName = p.Warehouse.WarehouseName

                }).ToListAsync();
                
            if (products.Count > 0) 
            {
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ViewProduct? product = await _stockApiDBContext.Products
                .Include(p => p.Category)
                .Include(p => p.Warehouse)
                .Where(p => p.Id == id)
                .Select(p => new ViewProduct
                {
                    //Id = p.Id,
                    ProductName = p.ProductName,
                    Count = p.Count,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    WarehouseName = p.Warehouse.WarehouseName

                }).FirstOrDefaultAsync();

            if (product!= null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            ViewProduct? product = await _stockApiDBContext.Products
                .Include(p => p.Category)
                .Include(p => p.Warehouse)
                .Where(p => p.ProductName == name)
                .Select(p => new ViewProduct
                {
                    //Id = p.Id,
                    ProductName = p.ProductName,
                    Count = p.Count,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    WarehouseName = p.Warehouse.WarehouseName
                }).FirstOrDefaultAsync();

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateProduct([FromBody] ViewProduct viewProduct)
        {
            Category? category = await _stockApiDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == viewProduct.CategoryName );
           
            Warehouse? warehouse = await _stockApiDBContext.Warehouses.FirstOrDefaultAsync(w => w.WarehouseName == viewProduct.WarehouseName);

            Product product = new Product()
            {
                ProductName = viewProduct.ProductName,
                Count = viewProduct.Count,
                Price = viewProduct.Price,
                WarehouseId = warehouse.Id
            };

            if (category != null)
            {
                product.CategoryId = category.Id;
            }
            else 
            {
                /*
                Category newCategory = new Category()
                {
                    CategoryName = viewProduct.CategoryName
                };
                await _stockApiDBContext.Categories.AddAsync(newCategory);
                await _stockApiDBContext.SaveChangesAsync();
                Category? lastAddedCategory = await _stockApiDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == viewProduct.CategoryName);
                product.CategoryId = lastAddedCategory.Id;
                */
                product.Category = new Category()
                {
                    CategoryName = viewProduct.CategoryName
                };

            }
         
            if (product != null)
            {
                _stockApiDBContext.Products.Add(product);
                await _stockApiDBContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProduct([FromBody] ViewProduct viewProduct)
        {
            Product? product = await _stockApiDBContext.Products.FirstOrDefaultAsync(p => p.ProductName == viewProduct.ProductName);
            Category? category = await _stockApiDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == viewProduct.CategoryName);
            Warehouse? warehouse = await _stockApiDBContext.Warehouses.FirstOrDefaultAsync(w => w.WarehouseName == viewProduct.WarehouseName);

            if (product != null)
            {
                if (viewProduct.Count != null)
                    product.Count = viewProduct.Count;
                if (viewProduct.Price != null)
                    product.Price = viewProduct.Price;
                if (viewProduct.CategoryName != null)
                    product.Category = category;
                if (viewProduct.WarehouseName != null)
                    product.Warehouse = warehouse;
                await _stockApiDBContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound("Bu ürün bulunmamaktadır");
            }
        }

        [HttpDelete]
        [Route("[action]/{productName}")]
        public async Task<IActionResult> RemoveProduct(string productName)
        {
            Product? product = await _stockApiDBContext.Products.FirstOrDefaultAsync(p => p.ProductName == productName);

            if (product != null)
            {
                _stockApiDBContext.Remove(product);
                await _stockApiDBContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound("Bu ürün bulunmamaktadır");
            }
        }
    }
}
