namespace Product.Domain.Entity
{
	public class Product : IAggregateRoot
	{
		public Guid Id { get; init; }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public string ImageUrl { get; private set; }
		public Category? Category { get; private set; }
		public Guid CategoryId { get; private set; }

		public List<ProductVariant> Variants { get; private set; }

		public bool Featured { get; private set; }
		public bool Visible { get; private set; }
		public bool Deleted { get; private set; }

		private Product()
		{

		}

		public Product(string title, string description, string imageUrl, Guid categoryId, List<ProductVariant> variants, bool featured = false, bool visible = true, bool deleted = false)
		{
			Id = Guid.NewGuid();
			Title = title;
			Description = description;
			ImageUrl = imageUrl;
			CategoryId = categoryId;
			Variants = variants;
			Featured = featured;
			Visible = visible;
			Deleted = deleted;
		}

		public void ActiveFeatured()
		{
			if (Featured)
			{
				Console.WriteLine("该商品已经是特色商品");
				return;
			}
			this.Featured = true;
		}

		public void DrawbackFeatured()
		{
			if (!Featured)
			{
				Console.WriteLine("该商品已不是特色商品");
				return;
			}
			this.Featured = false;
		}

		public void AddVariant(ProductVariant variant)
		{
			if (this.Variants.Contains(variant))
			{
				Console.WriteLine("该商品已包含该套餐");
				return;
			}
			this.Variants.Add(variant);
		}

		public void RemoveVariant(ProductVariant variant)
		{
			if (!this.Variants.Contains(variant))
			{
				Console.WriteLine("该商品不包含该套餐");
				return;
			}
			this.Variants.Remove(variant);
		}

		public void ProductVisible()
		{
			if (Visible)
			{
				Console.WriteLine("该商品已是可见状态");
				return;
			}
			this.Visible = true;
		}

		public void ProductInVisible()
		{
			if (!Visible)
			{
				Console.WriteLine("该商品已是不可见状态");
				return;
			}
			this.Visible = false;
		}

		public void ProductDeleted()
		{
			if (Deleted)
			{
				Console.WriteLine("该商品已删除");
				return;
			}
			this.Visible = false;
			this.Deleted = true;
		}
	}
}
