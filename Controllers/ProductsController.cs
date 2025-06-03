// Products Controller
// Author: Gabriel Demetrios Lafis

using Microsoft.AspNetCore.Mvc;

namespace CSharp_Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
        new Product { Id = 2, Name = "Mouse", Price = 29.99m, Category = "Electronics" },
        new Product { Id = 3, Name = "Keyboard", Price = 79.99m, Category = "Electronics" }
    };

    [HttpGet]
    public ActionResult<ApiResponse<IEnumerable<Product>>> GetProducts()
    {
        return Ok(new ApiResponse<IEnumerable<Product>>
        {
            Success = true,
            Message = "Products retrieved successfully",
            Data = Products,
            Author = "Gabriel Demetrios Lafis"
        });
    }

    [HttpGet("{id}")]
    public ActionResult<ApiResponse<Product>> GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound(new ApiResponse<Product>
            {
                Success = false,
                Message = "Product not found",
                Author = "Gabriel Demetrios Lafis"
            });
        }

        return Ok(new ApiResponse<Product>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = product,
            Author = "Gabriel Demetrios Lafis"
        });
    }

    [HttpPost]
    public ActionResult<ApiResponse<Product>> CreateProduct(Product product)
    {
        product.Id = Products.Max(p => p.Id) + 1;
        Products.Add(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, 
            new ApiResponse<Product>
            {
                Success = true,
                Message = "Product created successfully",
                Data = product,
                Author = "Gabriel Demetrios Lafis"
            });
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public string Author { get; set; } = "Gabriel Demetrios Lafis";
}
