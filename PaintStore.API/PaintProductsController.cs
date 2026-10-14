using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
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
    }
}
