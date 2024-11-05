using System.Text;
using Microsoft.Extensions.Configuration;
namespace WeixinSDK.ChatRobot.Tests;

public class Tests
{
    private IConfiguration _configuration;
    private AppSettings _appSettings;
    
    [SetUp]
    public void Setup()
    {
        // 构建配置
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        _configuration = builder.Build();

        _appSettings = _configuration.GetRequiredSection("AppSettings").Get<AppSettings>();
    }

    [Test]
    public async Task TestSendMessageByMobile()
    {
        string webhookUrl = _appSettings.Webhook;
        var mobileList = new List<string>() { "15213237601" };
        var message = new
        {
            msgtype = "text",
            text = new
            {
                content = "生产延期预警，生产可能出现错误，请及时处理",
                mentioned_mobile_list = Newtonsoft.Json.JsonConvert.SerializeObject(mobileList),
            }
        };
        string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        using (HttpClient client = new HttpClient())
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(webhookUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();
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
        string webhookUrl = _appSettings.Webhook;
        var message = new
        {
            msgtype = "text",
            text = new
            {
                content = "你好，ATE的大家，我是一个机器人"
            }
        };
        string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        using (HttpClient client = new HttpClient())
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(webhookUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();
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
    /// 测试配置文件是否存在
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
        return webhookUrl??string.Empty;
    }
}