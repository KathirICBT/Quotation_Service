using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplier_A.Model;
using Supplier_A.ProductService.Data;

namespace Supplier_A.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<List<ProductModel>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductModel> GetProduct(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductModel product, IFormFile? file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/images", file.FileName);

                    // Ensure directory exists
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    product.ImageUrl = $"/images/{file.FileName}"; // Save the image URL in the product
                }
                else
                {
                    product.ImageUrl = null; // Set to null or a default value if no image is uploaded
                }

                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        
        // New method to serve images
        [HttpGet("image/{fileName}")]
        public IActionResult GetProductImage(string fileName)
        {
            var filePath = Path.Combine(_imagePath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var image = System.IO.File.OpenRead(filePath);
            var contentType = GetContentType(fileName);
            return File(image, contentType);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }


    }
}
