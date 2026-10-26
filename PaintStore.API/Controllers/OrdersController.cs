using System.Diagnostics.CodeAnalysis;
using System.Xml.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PaintStore.API.DTOs;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orderscontroller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Orderscontroller(AppDbContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
            if (user == null) return BadRequest();

            if (request.PaintProducts == null || !request.PaintProducts.Any() || request.PaintProducts.Any(p => p.Stock < 0)) return BadRequest();

            var existingProducts = new List<PaintProduct>();

            foreach (var product in request.PaintProducts)
            {
                var existingProduct = _context.PaintProducts.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return BadRequest();
                }

                existingProducts.Add(existingProduct);
            }

            var newOrder = new Order();

            newOrder.UserId = request.UserId;
            newOrder.PaintProducts = request.PaintProducts;
            newOrder.TotalPrice = request.TotalPrice;
            newOrder.Quantity = request.Quantity;
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrdersByUserId), new { userId = newOrder.UserId }, newOrder);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderResponseDTO>> GetAllOrder()
        {
            var orders = _context.Orders
            .Select(o => new OrderResponseDTO
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status
            })
            .ToList();

            return Ok(orders);

        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder([FromBody] UpdateOrderRequest request, int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            if (request.TotalAmount < 0) return BadRequest();

            order.OrderId = request.OrderId;
            order.UserId = request.UserId;
            order.OrderDate = request.OrderDate;
            order.TotalAmount = request.TotalAmount;
            order.Status =  request.Status;
            _context.SaveChanges();

            return Ok(order);
        }
    }
}
