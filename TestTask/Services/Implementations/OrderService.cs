using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Order> GetOrder()
        {
            var priceMax = await _applicationDbContext.Orders.MaxAsync(order => order.Price);
            var orderPriceMax = await _applicationDbContext.Orders.FirstOrDefaultAsync(order => order.Price == priceMax);
            return orderPriceMax;
        }

        public async Task<List<Order>> GetOrders()
        {
            const int orderQuantity = 10;
            var orders = await _applicationDbContext.Orders.Where(order => order.Quantity >= orderQuantity).ToListAsync<Order>();
            return orders;
        }
    }
}
