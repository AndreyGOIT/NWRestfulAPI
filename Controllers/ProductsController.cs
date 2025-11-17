using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Dependency Injection malli
        private NorthwindContext db;

        public ProductsController(NorthwindContext dbparametri) // konstruktorissa
        {
            db = dbparametri; // saadaan tietokantayhteys
        }

        [HttpGet]
        public ActionResult GetAllProducts()
        {
            var products = db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            var product = db.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("category/{id}")]
        public ActionResult GetProductsByCategory(int id)
        {
            var products = db.Products.Where(p => p.CategoryId == id).ToList();
            return Ok(products);
        }

        [HttpPost]
        public ActionResult AddProduct([FromBody] Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updated)
        {
            var product = db.Products.Find(id);
            if (product == null) return NotFound();

            product.ProductName = updated.ProductName;
            product.UnitPrice = updated.UnitPrice;
            product.UnitsInStock = updated.UnitsInStock;
            product.CategoryId = updated.CategoryId;

            db.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null) return NotFound();

            db.Products.Remove(product);
            db.SaveChanges();
            return Ok($"Product {id} deleted");
        }
    }
}
