using DocumentStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    ProductsDbContext dbContext,
    ILogger<Product> logger) : ControllerBase
{
    [HttpGet("read")]
    public IActionResult ReadProduct()
    {
        logger.LogInformation("Reading products");

        var products = dbContext.Products.ToList();

        if (!products.Any())
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpPost("create")]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        logger.LogInformation("Creating a new product");
        dbContext.Products.Add(product);
        dbContext.SaveChanges();

        return CreatedAtAction(nameof(ReadProduct), new { id = product.Id }, product);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteProduct([FromBody] int productId)
    {
        logger.LogInformation("Deleting a product");

        var product = dbContext.Products.Find(productId);
        if (product == null)
        {
            return NotFound();
        }

        dbContext.Products.Remove(product);
        dbContext.SaveChanges();

        return NoContent();
    }
}
