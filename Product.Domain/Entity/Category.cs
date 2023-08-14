namespace Product.Domain.Entity
{
	public class Category
	{
		public Guid Id { get; init; }
		public string Name { get; private set; }
		public string Url { get; private set; }

		public bool Visible { get; private set; }
		public bool Deleted { get; private set; }

		private Category()
		{

		}

		public Category(string name, string url, bool visible = true, bool deleted = false)
		{
			Id = Guid.NewGuid();
			Name = name;
			Url = url;
			Visible = visible;
			Deleted = deleted;
		}

		public void CategoryVisible()
		{
			if (Visible)
			{
				Console.WriteLine("该类别已经可见");
				return;
			}
			this.Visible = true;
		}

		public void CategoryInvisible()
		{
			if (!Visible)
			{
				Console.WriteLine("该类别已经不可见");
				return;
			}
			this.Visible = false;
		}

		public void CategoryDeleted()
		{
			if (Deleted)
			{
				Console.WriteLine("该类别已删除");
				return;
			}
			this.Visible = false;
			this.Deleted = true;
		}
	}
}
