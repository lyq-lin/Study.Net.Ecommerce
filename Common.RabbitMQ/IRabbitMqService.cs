namespace Common.RabbitMQ
{
	public interface IRabbitMqService
	{
		Task<string> ConsumeMessage(string exchange, string queueName, string routingKey, string type = "fanout");

		Task PublishMessage(string exchange, string queueName, string message, string routingKey, string type = "fanout");
	}
}
