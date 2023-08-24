using Order.Domain.Entity;

namespace Order.Domain
{
	public class OrderDomainService
	{
		private readonly IOrderRepository _orderRepository;

		public OrderDomainService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<Entity.Order> CreateOrder(Guid outTradeNo, Guid userId, decimal totalPrice)
		{
			var isExist = await _orderRepository.GetOrderDetail(outTradeNo, userId);
			if (isExist != null)
			{
				return isExist;
			}

			Order.Domain.Entity.Order newOrder = new Entity.Order(outTradeNo, userId, totalPrice);

			return newOrder;
		}

		public async Task<OrderItem> CreateOrderItem(Entity.Order newOrder, Guid productId, Guid productTypeId, decimal price, int quantity)
		{
			return new OrderItem(newOrder, productId, productTypeId, price * quantity, quantity);
		}
	}
}
