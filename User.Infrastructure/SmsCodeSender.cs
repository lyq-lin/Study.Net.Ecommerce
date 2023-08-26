using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Tea;
using User.Domain;
using User.Domain.ValueObject;

namespace User.Infrastructure
{
	public class SmsCodeSender : ISmsCodeSender
	{
		public Task SendAsync(PhoneNumber phone, string code)
		{
			UseSms(phone.PhoneNumberValue, code);
			return Task.CompletedTask;
		}

		private static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient(string accessKeyId, string accessKeySecret)
		{
			AlibabaCloud.OpenApiClient.Models.Config config = new()
			{
				AccessKeyId = accessKeyId,
				AccessKeySecret = accessKeySecret
			};

			config.Endpoint = "dysmsapi.aliyuncs.com";
			return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
		}

		private static void UseSms(string phone, string token)
		{
			var client = CreateClient($@"输入你的KeyId", $@"输入你的KeySecret");

			SendSmsRequest sendSmsRequest = new SendSmsRequest()
			{
				PhoneNumbers = phone,
				SignName = "输入你的模板签名",
				TemplateCode = "输入你的模板Code",
				TemplateParam = $"{{\"code\":\"{token}\"}}"
			};

			try
			{
				client.SendSmsWithOptionsAsync(sendSmsRequest, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
			}
			catch (TeaException error)
			{

				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
			}
			catch (Exception e)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message",e.Message}
				});

				AlibabaCloud.TeaUtil.Common.AssertAsString(e.Message);
			}
		}
	}
}
