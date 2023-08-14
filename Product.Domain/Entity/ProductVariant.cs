using System.Text.Json.Serialization;

namespace Product.Domain.Entity
{
	public class ProductVariant : IAggregateRoot
	{
		public Guid Id { get; init; }

		[JsonIgnore]
		public Product? Product { get; private set; }
		public Guid ProductId { get; private set; }

		public ProductType? ProductType { get; private set; }
		public Guid ProductTypeId { get; private set; }

		public decimal Price { get; private set; }
		public decimal OriginalPrice { get; private set; }

		public bool Visible { get; private set; }

		public bool Deleted { get; private set; }


		private ProductVariant()
		{

		}

		public ProductVariant(Guid productId, Guid typeId, decimal originalPrice, decimal price, bool visible = true, bool deleted = false)
		{
			Id = Guid.NewGuid();
			ProductId = productId;
			ProductTypeId = typeId;
			OriginalPrice = originalPrice;
			Price = price;
			Visible = visible;
			Deleted = deleted;
		}

		public void UpdateType(ProductType type)
		{
			if (type.Name == ProductType.Name)
			{
				Console.WriteLine("已存在该套餐");
				return;
			}
			ProductType = type;
		}

		public void VariantVisible()
		{
			if (Visible)
			{
				Console.WriteLine("该套餐已经是可见状态");
				return;
			}
			this.Visible = true;
		}

		public void VariantInvisible()
		{
			if (!Visible)
			{
				Console.WriteLine("该套餐已经是不可见状态");
				return;
			}
			this.Visible = false;
		}

		public void ProductDeteled()
		{
			if (Deleted)
			{
				Console.WriteLine("该套餐已删除");
				return;
			}

			this.Visible = false;
			this.Deleted = true;
		}
	}
}
