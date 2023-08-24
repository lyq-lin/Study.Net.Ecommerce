namespace Cart.WebAPI
{
	[AttributeUsage(AttributeTargets.Method)]
	public class UnitOfWorkAttribute : Attribute
	{
		public Type[] DbContextTypes { get; init; }
		public UnitOfWorkAttribute(params Type[] dbContextTypes)
		{
			DbContextTypes = dbContextTypes;
		}
	}
}
