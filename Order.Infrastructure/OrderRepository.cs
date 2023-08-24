using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Domain.Entity;
using Order.Infrastructure.DbContexts;

namespace Order.Infrastructure
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderDbContext _dbContext;

		public OrderRepository(OrderDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Domain.Entity.Order> GetOrderDetail(Guid orderId, Guid userId)
		{
			var result = await _dbContext.Orders.Where(o => o.UserId == userId && o.Id == orderId).Include(oi => oi.OrderItems).OrderByDescending(o => o.OrderDate).FirstOrDefaultAsync();

			return result;
		}

		public async Task<List<OrderItem>> GetOrderDetails(Guid orderId)
		{
			var result = await _dbContext.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();

			return result;
		}

		public async Task<List<Domain.Entity.Order>> GetOrders(Guid userId)
		{
			var result = await _dbContext.Orders.Where(o => o.UserId == userId).Include(oi => oi.OrderItems).OrderByDescending(o => o.OrderDate).ToListAsync();

			return result;
		}
	}
}
