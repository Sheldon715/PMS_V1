using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.API.DTOs;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaintProductsController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public List<PaintProduct> GetAllPaintProducts(
            List<PaintProduct> paintProducts,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber
        )
        {
            return paintProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }

        [HttpGet]
        public List<PaintProduct> GetProductsByPriceRange(
            List<PaintProduct> paintProducts,
            [FromQuery] int min,
            [FromQuery] int max
        )
        {
            return paintProducts
            .Where(p => p.Price > min && p.Price < max)
            .ToList();
        }

        [HttpGet]
        public List<PaintProduct> GetPaintProductsByPaintId(
            [FromQuery] int id,
            List<PaintProduct> paintProducts)
        {
            return paintProducts
            .Where(p => p.Id == id)
            .ToList();
        }

        [HttpGet("user/{id}")]
        public List<PaintProduct> GetPaintProductsByUserId(
            int id,
            List<PaintProduct> paintProducts)
        {
            return paintProducts
            .Where(p => p.UserId == id)
            .ToList();
        }

        [HttpPost]
        public ActionResult<PaintProduct> CreatePaintProduct([FromBody] CreatePaintProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Brand) ||
            request.Price <= 0 || request.Stock < 0) return BadRequest();

            if (_context.PaintProducts.FirstOrDefault(p => p.Name == request.Name) != null) return BadRequest();

            var paintProduct = new PaintProduct();

            paintProduct.Id = request.Id;
            paintProduct.Name = request.Name;
            paintProduct.Brand = request.Brand;
            paintProduct.Price = request.Price;
            paintProduct.Stock = request.Stock;
            _context.PaintProducts.Add(paintProduct);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPaintProductsByPaintId), new { id = paintProduct.Id }, paintProduct);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaintProductResponseDTO>> GetAllPaintProducts()
        {
            var allPaintProducts = _context.PaintProducts
            .Select(p => new PaintProductResponseDTO
            {
                Name = p.Name,
                Brand = p.Brand,
                Price = p.Price,
                Stock = p.Stock
            })
            .ToList();

            return Ok(allPaintProducts);
        }

        [HttpGet("{id}")]
        public ActionResult<PaintProduct> GetPaintProductById(int id)
        {
            var paintProduct = _context.PaintProducts.FirstOrDefault(p => p.Id == id);

            if (paintProduct == null) return NotFound();

            return Ok(paintProduct);
        }

        [HttpPut("{id}")]
        public ActionResult<PaintProduct> UpdatePaintProduct([FromBody] UpdatePaintProductRequest request, int id)
        {
            var paintProduct = _context.PaintProducts.FirstOrDefault(p => p.Id == id);

            if (paintProduct == null) return NotFound();

            if (string.IsNullOrWhiteSpace(request.Name) ||
            request.Price <= 0 || request.Stock < 0) return BadRequest();

            paintProduct.Id = request.Id;
            paintProduct.Name = request.Name;
            paintProduct.Price = request.Price;
            paintProduct.Stock = request.Stock;
            _context.SaveChanges();

            return Ok(paintProduct);
        }
    }
}
