using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ordersontroller : ControllerBase
    {

        [HttpGet]
        public List<Order> GetAllOrders(
            List<Order> orders,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber
        )
        {
            return orders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }

        [HttpGet]
        public List<Order> GetOrdersByPriceRange(
            List<Order> orders,
            [FromQuery] int min,
            [FromQuery] int max
        )
        {
            return orders
            .Where(o => o.TotalPrice > min && o.TotalPrice < max)
            .ToList();
        }

        [HttpGet("piant-product/{id}")]
        public List<Order> GetOrdersByPaintId(int id, List<Order> orders)
        {
            return orders
            .Where(o => o.PaintProducts.Any(p => p.Id == id))
            .ToList();
        }

        [HttpGet("user/{id}")]
        public List<Order> GetOrdersByUserId(int id, List<Order> orders)
        {
            return orders
            .Where(o => o.UserId == id)
            .ToList();
        }

        [HttpGet]
        public List<Order> GetLastMonthOrders(List<Order> orders)
        {   
            var now = DateTime.Now;
            var monthStartDate = new DateTime(now.Year, now.Month, 1);
            var lastMonthStartDate = monthStartDate.AddMonths(-1);

            return orders
            .Where(o => o.CreatedDate >= lastMonthStartDate 
            && o.CreatedDate <= monthStartDate)
            .ToList();
        }

        [HttpGet]
        public List<Order> GetLastMonthOrders(
            List<Order> orders,
            [FromQuery] DateTime date
        )
        {
            return orders
            .Where(o => o.CreatedDate == date)
            .ToList();
        }

    }
}
