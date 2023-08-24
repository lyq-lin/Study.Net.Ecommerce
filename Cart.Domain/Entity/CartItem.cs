namespace Cart.Domain.Entity
{
	public class CartItem
	{
		public Guid Id { get; init; }

		public Guid UserId { get; private set; }

		public Guid ProductId { get; init; }

		public Guid ProductTypeId { get; init; }

		public int Quantity { get; private set; }

		private CartItem()
		{

		}

		public CartItem(Guid productId, Guid productTypeId, int quantity = 1)
		{
			Id = Guid.NewGuid();
			ProductId = productId;
			ProductTypeId = productTypeId;
			Quantity = quantity;
		}

		public void UpdateQuantity(int quantity)
		{
			if (Quantity == quantity) return;

			Quantity = quantity;
		}

		public void SetUserId(Guid userId)
		{
			this.UserId = userId;
		}
	}
}
