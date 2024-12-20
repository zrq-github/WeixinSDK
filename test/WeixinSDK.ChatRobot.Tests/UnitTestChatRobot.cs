using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WeixinSDK.ChatRobot.Tests;

public class Tests
{
    private AppSettings _appSettings;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup()
    {
        // 构建配置
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);
        _configuration = builder.Build();

        _appSettings = _configuration.GetRequiredSection("AppSettings").Get<AppSettings>();
    }

    /// <summary>
    /// 测试根据手机号@指定人员
    /// </summary>
    [Test]
    public async Task TestSendMessageByMobile()
    {
        var webhookUrl = _appSettings.Webhook;
        var mobileList = new List<string> { "15213237601" };
        var message = new
        {
            msgtype = "text",
            text = new
            {
                content = "测试根据手机号@指定人员",
                mentioned_mobile_list = mobileList
            }
        };
        var jsonContent = JsonConvert.SerializeObject(message);
        using (var client = new HttpClient())
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(webhookUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseString}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    [Test]
    public async Task TestSendMessage()
    {
        var webhookUrl = _appSettings.Webhook;
        var message = new
        {
            msgtype = "text",
            text = new
            {
                content = "你好，ATE的大家，我是一个机器人"
            }
        };
        var jsonContent = JsonConvert.SerializeObject(message);
        using (var client = new HttpClient())
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(webhookUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseString}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    /// <summary>
    ///     测试配置文件是否存在
    /// </summary>
    [Test]
    public void TestWithConfigObject()
    {
        // 绑定配置
        var setting1 = _configuration.GetSection("MyConfig").GetSection("Setting1");
        var str = setting1.Value ?? string.Empty;
        // 断言
        Assert.That(str, Is.EqualTo("Value1"));
    }

    private string GetWebHookUrl()
    {
        var webhookUrl = _configuration["WeixinSDK.RobotWebhook"];
        return webhookUrl ?? string.Empty;
    }
}