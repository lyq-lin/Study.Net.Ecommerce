using Aop.Api;
using Aop.Api.Request;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Alipay
{
	public static class AlipayExtensions
	{
		public static IServiceCollection AddAlipay(this IServiceCollection services, AlipaySetting alipay)
		{
			AlipayConfig config = new AlipayConfig()
			{
				PrivateKey = alipay.privateKey,
				AlipayPublicKey = alipay.alipayPublicKey,
				ServerUrl = "https://openapi-sandbox.dl.alipaydev.com/gateway.do",
				AppId = "9021000122698272",
				Format = "json",
				Charset = "utf-8",
				SignType = "RSA2"
			};

			IAopClient alipayClient = new DefaultAopClient(config);

			AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();

			//支付成功后，返回到的前端地址
			request.SetReturnUrl("http://43.143.170.48:8080/#/payment-success");

			//支付状态异步回调地址 : 必须是支付宝服务器能访问到的公网地址
			//todo : 端口
			request.SetNotifyUrl("http://43.143.170.48:5172/api/Order/AlipayNotify");

			services.AddSingleton<AlipayTradePagePayRequest>(request);
			services.AddSingleton<IAopClient>(alipayClient);

			return services;
		}
	}
}
