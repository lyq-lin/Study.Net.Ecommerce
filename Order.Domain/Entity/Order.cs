using System.Text.Json.Serialization;

namespace Order.Domain.Entity
{
	public class Order
	{
		public Guid Id { get; init; }
		public DateTime OrderDate { get; init; }
		public Guid UserId { get; private set; }
		public decimal TotalPrice { get; private set; }

		[JsonIgnore]
		public List<OrderItem> OrderItems { get; private set; }

		private Order()
		{

		}

		public Order(Guid orderId, Guid userId, decimal totalPrice)
		{
			Id = orderId;
			OrderDate = DateTime.UtcNow;
			UserId = userId;
			TotalPrice = totalPrice;
			OrderItems = new List<OrderItem>();
		}

		public void AddOrderItem(OrderItem item)
		{
			OrderItems.Add(item);
		}

		public void SetTotalPrice(decimal totalPrice)
		{
			TotalPrice = totalPrice;
		}
		public void SetUser(Guid userId)
		{
			UserId = userId;
		}
	}
}
