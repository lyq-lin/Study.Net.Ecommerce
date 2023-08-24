using System.Text.Json.Serialization;

namespace Order.Domain.Entity
{
	public class OrderItem
	{
		[JsonIgnore]
		public Order Order { get; private set; }
		public Guid OrderId { get; private set; }
		public Guid ProductId { get; private set; }
		public Guid ProductTypeId { get; private set; }
		public int Quantity { get; private set; }
		public decimal TotalPrice { get; private set; }

		private OrderItem()
		{

		}

		public OrderItem(Order order, Guid productId, Guid prodcutTypeId, decimal totalPrice, int quantity = 1)
		{
			Order = order;
			ProductId = productId;
			ProductTypeId = prodcutTypeId;
			TotalPrice = totalPrice;
			Quantity = quantity;

			Order.AddOrderItem(this);
		}
	}
}
