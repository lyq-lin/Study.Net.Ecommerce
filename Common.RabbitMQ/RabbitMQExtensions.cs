using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Common.RabbitMQ
{
	public static class RabbitMQExtensions
	{
		public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
		{
			var factory = new ConnectionFactory()
			{
				HostName = "43.143.170.48",
				Port = 5672,
				UserName = "guest",
				Password = "guest",
				DispatchConsumersAsync = true
			};

			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();
			services.AddSingleton<IModel>(channel);
			return services;
		}
	}
}
