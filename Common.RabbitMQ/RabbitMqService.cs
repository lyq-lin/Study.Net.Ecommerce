using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.RabbitMQ
{
	public class RabbitMqService : IRabbitMqService
	{
		private readonly IModel _channel;

		public RabbitMqService(IModel model)
		{
			_channel = model;
		}

		public async Task<string> ConsumeMessage(string exchange, string queueName, string routingKey, string type = "fanout")
		{
			string message = string.Empty;
			_channel.ExchangeDeclare(exchange, type);
			_channel.QueueDeclare(queueName, false, false, false, null);
			_channel.QueueBind(queueName, exchange, routingKey);

			var tcs = new TaskCompletionSource<string>();

			var custom = new AsyncEventingBasicConsumer(_channel);

			custom.Received += async (model, _event) =>
			{
				var body = _event.Body;
				message = Encoding.UTF8.GetString(body.ToArray());

				tcs.SetResult(message);
			};

			_channel.BasicConsume(queueName, true, custom);

			var timeoutTask = Task.Delay(TimeSpan.FromSeconds(1));
			var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

			if (completedTask == timeoutTask)
			{
				Console.WriteLine("没有消息可消费");
			}

			_channel.QueueDelete(queueName);
			return message;
		}

		public Task PublishMessage(string exchange, string queueName, string message, string routingKey, string type = "fanout")
		{
			_channel.ExchangeDeclare(exchange, type);
			_channel.QueueDeclare(queueName, false, false, false, null);
			_channel.QueueBind(queueName, exchange, routingKey);

			var body = Encoding.UTF8.GetBytes(message);

			if (type.Equals("fanout"))
			{
				_channel.BasicPublish(exchange, queueName, null, body);
			}
			else
			{
				_channel.BasicPublish(exchange, routingKey, null, body);
			}

			return Task.CompletedTask;
		}

		public Task SubscribeMessage(string exchange, string queueName, string routingKey, Action<string> del, string type = "direct")
		{
			_channel.ExchangeDeclare(exchange, type);
			_channel.QueueDeclare(queueName, false, false, false, null);
			_channel.QueueBind(queueName, exchange, routingKey);

			var customer = new AsyncEventingBasicConsumer(_channel);

			customer.Received += async (model, _event) =>
			{
				var body = _event.Body;
				string message = Encoding.UTF8.GetString(body.ToArray());
				del(message);
			};

			_channel.BasicConsume(queueName, true, customer);

			return Task.CompletedTask;
		}
	}
}
